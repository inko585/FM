using AE.Graphics.Wpf.Basis;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace AE.Graphics.Wpf
{
    public enum AEWindowState
    {
        Normal,
        Minimized,
        Maximized
    }

    public interface ITimeOutAction
    {
        void OnTimeOut();
        bool TerminateSession { get; }
    }

    public class TimeOutAction : ITimeOutAction
    {
        public TimeOutAction(bool terminate, Action onTimeOut)
        {
            TerminateSession = terminate;
            this.onTimeOut = onTimeOut;
        }

        private readonly Action onTimeOut;
        public bool TerminateSession { get; private set; }

        public void OnTimeOut()
        {
            onTimeOut();
        }
    }

    public class DefaultTimeOutAction : ITimeOutAction
    {
        public virtual bool TerminateSession
        {
            get
            {
                return true;
            }
        }

        public virtual void OnTimeOut()
        {
            // nothing
        }
    }

    public class AEWindow : Window, IDisposable
    {

        public static string ApplicationIcon { get; set; }
        public static readonly DependencyProperty TimeOutActiveProperty = DependencyProperty.Register("TimeOutActive", typeof(bool), typeof(AEWindow), new UIPropertyMetadata(false, TimeOutActivePropertyChanged));
        public static readonly DependencyProperty TimeOutSecondsProperty = DependencyProperty.Register("TimeOutSeconds", typeof(int), typeof(AEWindow), new UIPropertyMetadata(0, TimeOutSecondsPropertyChanged));
        public static readonly DependencyProperty TimeOutActionProperty = DependencyProperty.Register("TimeOutAction", typeof(ITimeOutAction), typeof(AEWindow), new UIPropertyMetadata(null, TimeOutActionPropertyChanged));

        private static void TimeOutActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AEWindow).TimeOutActive = (bool)e.NewValue;
        }
        private static void TimeOutSecondsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AEWindow).TimeOutSeconds = (int)e.NewValue;
        }
        private static void TimeOutActionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AEWindow).TimeOutAction = (ITimeOutAction)e.NewValue;
        }

        public ITimeOutAction TimeOutAction { get; set; }

        public bool TimeOutActive
        {
            get
            {
                return timeOutActive;
            }
            set
            {
                timeOutActive = value;
                if (timeOutActive && !timer.IsEnabled)
                {
                    timer.Start();
                }
                else
                {
                    if (!timeOutActive && timer.IsEnabled)
                    {
                        timer.Stop();
                    }
                }
            }

        }
        public int TimeOutSeconds
        {
            get
            {

                return timeOutTimeSeconds;
            }
            set
            {
                timeOutTimeSeconds = value;
                timer.Interval = TimeSpan.FromSeconds(timeOutTimeSeconds);
            }
        }

        // ---------- private fields ----------
        private readonly bool closeButtonHidden;

        private AEWindowState AEWindowState { get; set; }

        private double normalTop;
        private double normalLeft;
        private double normalWidth;
        private double normalHeight;

        private int timeOutTimeSeconds;
        private bool timeOutActive;

        private readonly DispatcherTimer timer;

        private bool saveWindowPosition;


        // ---------- public properties ----------
#pragma warning disable S2292 // Trivial properties should be auto-implemented
        public bool SaveWindowPosition
#pragma warning restore S2292 // Trivial properties should be auto-implemented
        {
            get { return this.saveWindowPosition; }
            set { this.saveWindowPosition = value; }
        }

        public BaseViewModel ViewModel { get; set; }

        // ---------- public constructor ----------
        public AEWindow()
        {
            this.AllowsTransparency = true;
            timeOutTimeSeconds = 120;
            timeOutActive = false;
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(timeOutTimeSeconds * 1000)
            };
            timer.Tick += TimerElapsed;

            InitializeStyle();

            this.Loaded += AEWindow_Loaded;
            this.ContentRendered += AEWindow_ContentRendered;

            this.saveWindowPosition = true;

            TimeOutAction = new DefaultTimeOutAction();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        public AEWindow(BaseViewModel viewModel)
            : this()
        {
            this.ViewModel = viewModel;
            this.ViewModel.CloseRequest += delegate
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = this.ViewModel;
        }

        public AEWindow(bool closeButtonHidden)
            : this()
        {
            this.closeButtonHidden = closeButtonHidden;
        }

        // ---------- private event methods ----------
        private void AEWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeStyle();

            OnStateChanged(e);
        }

        private void AEWindow_ContentRendered(object sender, EventArgs e)
        {
            InitializeWindowButtons();
            InitializeWindowBorderEvents();

            LoadLastWindowPosition();
        }

        public void TimerElapsed(object sender, EventArgs e)
        {
            if (this.IsVisible)
            {
                using (var messageBox = new AE.Graphics.Wpf.View.AEMessageBoxView())
                {
                    messageBox.AssignViewModel(new AE.Graphics.Wpf.ViewModel.LDCountdownMessageViewModel("Die Sitzung ist seit nun seit einiger Zeit inaktiv. Um Lizenzen zu sparen wird das Makro nach Ablauf des Timers automatisch beendet. Klicken Sie auf 'Abbrechen' um fortzufahren.", "The session has been idle for some time. In order to save licences, the makro will terminate automatically after timer runs out. Click 'Cancel' to continue."));
                    if (messageBox.ShowDialog() == true)
                    {
                        TimeOutAction.OnTimeOut();

                        if (TimeOutAction.TerminateSession)
                        {
                            foreach (Window w in Application.Current.Windows)
                            {
                                if (w is AEWindow)
                                {
                                    w.DialogResult = false;
                                    w.Close();
                                }
                            }
                        }


                    }
                }
            }
        }

        // ---------- public methods ----------
        public void InitializeStyle()
        {
            this.Resources.Source = new Uri("/AE.Graphics.Wpf;component/Styles/AEStyles.xaml", UriKind.RelativeOrAbsolute);
            this.Style = (Style)this.Resources["AEWindowStyle"];
            if (ApplicationIcon != null && this.Resources.Contains(ApplicationIcon))
            {
                GetTemplateControl<ContentPresenter>("AppIcon").Content = Resources[ApplicationIcon];
            }
            TryToSetBackgroundColor();
        }

        public void InitializeWindowButtons()
        {
            var minimizeWindowButton = GetTemplateControl<Button>("MinimizeWindowButton");
            var maximizeWindowButton = GetTemplateControl<Button>("MaximizeWindowButton");
            var closeWindowButton = GetTemplateControl<Button>("CloseWindowButton");

            minimizeWindowButton.Visibility = Visibility.Visible;
            maximizeWindowButton.Visibility = Visibility.Visible;
            closeWindowButton.Visibility = Visibility.Visible;

            if (ResizeMode == ResizeMode.NoResize)
            {
                minimizeWindowButton.Visibility = Visibility.Hidden;
                maximizeWindowButton.Visibility = Visibility.Hidden;
            }
            else if (ResizeMode == ResizeMode.CanMinimize)
            {
                minimizeWindowButton.Visibility = Visibility.Visible;
                maximizeWindowButton.IsEnabled = false;
            }
            else
            {
                minimizeWindowButton.Visibility = Visibility.Visible;
                maximizeWindowButton.Visibility = Visibility.Visible;
            }

            if (this.closeButtonHidden)
                closeWindowButton.Visibility = Visibility.Hidden;

            closeWindowButton.Click += CloseButtonClickedEvent;
            maximizeWindowButton.Click += MaximizeButtonClickedEvent;
            minimizeWindowButton.Click += MinimzeButtonClickedEvent;
        }

        public void InitializeWindowBorderEvents()
        {
            SetThumbProperties(GetTemplateControl<Thumb>("LeftTopBorderThumb"), Cursors.SizeNWSE);
            SetThumbProperties(GetTemplateControl<Thumb>("LeftBorderThumb"), Cursors.SizeWE);
            SetThumbProperties(GetTemplateControl<Thumb>("LeftBottomBorderThumb"), Cursors.SizeNESW);

            SetThumbProperties(GetTemplateControl<Thumb>("TopLeftBorderThumb"), Cursors.SizeNWSE);
            SetThumbProperties(GetTemplateControl<Thumb>("TopBorderThumb"), Cursors.SizeNS);
            SetThumbProperties(GetTemplateControl<Thumb>("TopRightBorderThumb"), Cursors.SizeNESW);

            SetThumbProperties(GetTemplateControl<Thumb>("RightTopBorderThum"), Cursors.SizeNESW);
            SetThumbProperties(GetTemplateControl<Thumb>("RightBorderThumb"), Cursors.SizeWE);
            SetThumbProperties(GetTemplateControl<Thumb>("RightBottomBorderThumb"), Cursors.SizeNWSE);

            SetThumbProperties(GetTemplateControl<Thumb>("BottomLeftBorderThumb"), Cursors.SizeNESW);
            SetThumbProperties(GetTemplateControl<Thumb>("BottomBorderThumb"), Cursors.SizeNS);
            SetThumbProperties(GetTemplateControl<Thumb>("BottomRightBorderThumb"), Cursors.SizeNWSE);

            var windowBar = GetTemplateControl<DockPanel>("WindowBar");
            windowBar.MouseDown += WindowBarMouseDownEvent;
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            var windowBar = GetTemplateControl<DockPanel>("WindowBar");
            _ = GetTemplateControl<Button>("MinimizeWindowButton");

            if (Mouse.GetPosition(this).Y <= windowBar.ActualHeight)
            {
                if (this.AEWindowState == AEWindowState.Maximized)
                {
                    NormalizeWindow();
                }
                else
                {
                    if (this.ResizeMode != ResizeMode.NoResize)
                        MaximizeWindow();
                }
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                timer.Start();
            }
            base.OnPreviewMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                timer.Start();
            }

            base.OnKeyDown(e);
        }
        // ---------- private methods ----------
        private void TryToSetBackgroundColor()
        {
            try
            {
                var content = this.Content as Panel;

                if (content != null)
                {
                    content.Background = (Brush)this.Resources["MainBackGroundColor"];
                }
            }
            catch
            {
                //return
            }
        }

        private ControlType GetTemplateControl<ControlType>(string name)
        {
            this.ApplyTemplate();
            var ctrlTemp = this.Resources["AEWindowBorderTemplate"] as ControlTemplate;
            var ctrl = ctrlTemp.FindName(name, this);

            return (ControlType)ctrl;
        }

        private void SetThumbProperties(Thumb thumb, Cursor cursor)
        {
            if (ResizeMode == ResizeMode.NoResize || ResizeMode == ResizeMode.CanMinimize || AEWindowState == AEWindowState.Maximized)
            {
                thumb.DragDelta -= ResizeDragDeltaEvent;
                thumb.Cursor = Cursors.Arrow;

                if (thumb.Name == "BottomRightBorderThumb")
                {
                    SetResizeGripHidden();
                }
            }
            else if (ResizeMode == ResizeMode.CanResize)
            {
                thumb.DragDelta += ResizeDragDeltaEvent;
                thumb.Cursor = cursor;

                if (thumb.Name == "BottomRightBorderThumb")
                {
                    SetResizeGripHidden();
                }
            }
            else if (ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                thumb.DragDelta += ResizeDragDeltaEvent;

                if (thumb.Name == "BottomRightBorderThumb")
                {
                    SetResizeGripVisible();
                }
            }
        }

        private void ResizeDragDeltaEvent(object sender, DragDeltaEventArgs e)
        {
            Thumb currentThumb = sender as Thumb;

            if (currentThumb.Name == "LeftBorderThumb")
            {
                ResizeLeft(e.HorizontalChange);
            }
            else if (currentThumb.Name == "TopBorderThumb")
            {
                ResizeTop(e.VerticalChange);
            }
            else if (currentThumb.Name == "RightBorderThumb")
            {
                ReszieRight(e.HorizontalChange);
            }
            else if (currentThumb.Name == "BottomBorderThumb")
            {
                ResizeBottom(e.VerticalChange);
            }
            else if (currentThumb.Name == "LeftTopBorderThumb" || currentThumb.Name == "TopLeftBorderThumb")
            {
                ResizeLeft(e.HorizontalChange);
                ResizeTop(e.VerticalChange);
            }
            else if (currentThumb.Name == "RightBottomBorderThumb" || currentThumb.Name == "BottomRightBorderThumb")
            {
                ReszieRight(e.HorizontalChange);
                ResizeBottom(e.VerticalChange);
            }
            else if (currentThumb.Name == "TopRightBorderThumb" || currentThumb.Name == "RightTopBorderThum")
            {
                ResizeTop(e.VerticalChange);
                ReszieRight(e.HorizontalChange);
            }
            else if (currentThumb.Name == "LeftBottomBorderThumb" || currentThumb.Name == "BottomLeftBorderThumb")
            {
                ResizeLeft(e.HorizontalChange);
                ResizeBottom(e.VerticalChange);
            }
        }

        private void WindowBarMouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (this.AEWindowState == AEWindowState.Maximized)
                {
                    this.normalTop = 0;
                    NormalizeWindow();
                }

                DragMove();
            }
        }

        private void SetResizeGripVisible()
        {
            var bottomBorderGrid = GetTemplateControl<Grid>("BottomBorderGrid");
            var bottomRightBorderThumb = GetTemplateControl<Thumb>("BottomRightBorderThumb");
            var bottomBorderResizeGrip = GetTemplateControl<ResizeGrip>("BottomBorderResizeGrip");

            bottomBorderGrid.Height = 20;
            bottomRightBorderThumb.Visibility = Visibility.Collapsed;
            bottomBorderResizeGrip.Visibility = Visibility.Visible;

        }

        private void SetResizeGripHidden()
        {
            var bottomBorderGrid = GetTemplateControl<Grid>("BottomBorderGrid");
            var bottomRightBorderThumb = GetTemplateControl<Thumb>("BottomRightBorderThumb");
            var bottomBorderResizeGrip = GetTemplateControl<ResizeGrip>("BottomBorderResizeGrip");

            bottomBorderGrid.Height = 3;
            bottomRightBorderThumb.Visibility = Visibility.Visible;
            bottomBorderResizeGrip.Visibility = Visibility.Collapsed;
        }

        // ---------- Resize Methods ----------
        private void ResizeLeft(double horizontalChange)
        {
            double deltaHorizontal = Math.Min(horizontalChange, this.ActualWidth - this.MinWidth);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + deltaHorizontal);

            if ((this.Width - deltaHorizontal) >= this.MinWidth && (this.Width - deltaHorizontal) >= 150)
            {
                this.Width -= deltaHorizontal;
            }
        }

        private void ResizeTop(double verticalChange)
        {
            double deltaVertical = Math.Min(verticalChange, this.ActualHeight - this.MinHeight);
            Canvas.SetTop(this, Canvas.GetTop(this) + deltaVertical);

            if ((this.Height - deltaVertical) >= this.MinHeight && (this.Height - deltaVertical) >= 100)
            {
                this.Height -= deltaVertical;
            }
        }

        private void ReszieRight(double horizontalChange)
        {
            double deltaHorizontal = Math.Min(-horizontalChange, this.ActualWidth - this.MinWidth);

            if ((this.Width - deltaHorizontal) >= this.MinWidth && (this.Width - deltaHorizontal) >= 150)
            {
                this.Width -= deltaHorizontal;
            }
        }

        private void ResizeBottom(double verticalChange)
        {
            double deltaVertical = Math.Min(-verticalChange, this.ActualHeight - this.MinHeight);

            if ((this.Height - deltaVertical) >= this.MinHeight && (this.Height - deltaVertical) >= 100)
            {
                this.Height -= deltaVertical;
            }
        }

        // ---------- Windowstate Methods ----------
        protected override void OnStateChanged(EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MaximizeWindow();
            }
            base.OnStateChanged(e);
        }

        protected void NormalizeWindow()
        {
            this.AEWindowState = AEWindowState.Normal;

            InitializeWindowBorderEvents();

            var maximizeIcon = GetTemplateControl<TextBlock>("MaximizeWindowButtonIcon");
            maximizeIcon.Text = "\uf2d0";

            this.Top = normalTop;
            this.Left = normalLeft;
            this.Width = normalWidth;
            this.Height = normalHeight;
        }
        protected void MinimizeWindow()
        {
            this.WindowState = WindowState.Minimized;
        }
        protected void MaximizeWindow(string screenDeviceName = "")
        {
            this.normalTop = this.Top;
            this.normalLeft = this.Left;
            this.normalWidth = this.ActualWidth;
            this.normalHeight = this.ActualHeight;

            this.AEWindowState = AEWindowState.Maximized;

            InitializeWindowBorderEvents();

            var maximizeIcon = GetTemplateControl<TextBlock>("MaximizeWindowButtonIcon");
            maximizeIcon.Text = "\uf2d2";

            var interopHelper = new WindowInteropHelper(this);
            var activeScreen = System.Windows.Forms.Screen.FromHandle(interopHelper.Handle);

            if (screenDeviceName != "")
            {
                var deviceNameScreen = System.Windows.Forms.Screen.AllScreens.FirstOrDefault(x => x.DeviceName == screenDeviceName);
                if (deviceNameScreen != null)
                {
                    activeScreen = deviceNameScreen;
                }
            }

            this.Top = activeScreen.WorkingArea.Top;
            this.Left = activeScreen.WorkingArea.Left;
            this.Width = activeScreen.WorkingArea.Width;
            this.Height = activeScreen.WorkingArea.Height;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            timer.Stop();
            base.OnClosing(e);
        }

        // ---------- Window Buttons ----------
        private void CloseButtonClickedEvent(object sender, RoutedEventArgs e)
        {
            SaveWindowPositions();

            this.Close();
        }
        private void MaximizeButtonClickedEvent(object sender, RoutedEventArgs e)
        {
            if (this.AEWindowState == AEWindowState.Normal)
                MaximizeWindow();
            else
                NormalizeWindow();
        }
        private void MinimzeButtonClickedEvent(object sender, RoutedEventArgs e)
        {
            MinimizeWindow();
        }

        // ---------- last window position ----------
        private void SaveWindowPositions()
        {
            if (this.Name != "" && this.SaveWindowPosition)
            {
                string regKeyName = @"HKEY_CURRENT_USER\Software\NexansAE\AEWindow\" + this.GetType().Assembly.GetName().Name + "\\" + this.Name;

                Registry.SetValue(regKeyName, "Top", this.Top);
                Registry.SetValue(regKeyName, "Left", this.Left);
                Registry.SetValue(regKeyName, "Height", this.Height);
                Registry.SetValue(regKeyName, "Width", this.Width);
                Registry.SetValue(regKeyName, "AEWindowState", this.AEWindowState);
                Registry.SetValue(regKeyName, "ActiveScreen", System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).DeviceName);
            }
        }
        private void LoadLastWindowPosition()
        {
            if (this.Name != "")
            {
                string regKeyName = @"HKEY_CURRENT_USER\Software\NexansAE\AEWindow\" + this.GetType().Assembly.GetName().Name + "\\" + this.Name;

                if (Registry.GetValue(regKeyName, "AEWindowState", this.AEWindowState) != null)
                {
                    this.AEWindowState = (AEWindowState)Enum.Parse(typeof(AEWindowState), (string)Registry.GetValue(regKeyName, "AEWindowState", this.WindowState));

                    if (this.AEWindowState == AEWindowState.Maximized)
                    {
                        string activeScreen = (string)Registry.GetValue(regKeyName, "ActiveScreen", "");

                        MaximizeWindow(activeScreen);
                    }
                    else
                    {
                        ////this.Top = Convert.ToInt32(Registry.GetValue(regKeyName, "Top", this.Top));
                        ////this.Left = Convert.ToInt32(Registry.GetValue(regKeyName, "Left", this.Left));
                        this.Height = Convert.ToDouble(Registry.GetValue(regKeyName, "Height", this.Height));
                        this.Width = Convert.ToDouble(Registry.GetValue(regKeyName, "Width", this.Width));
                    }
                }
            }
        }

        // ---------- idisposable interface ----------
        bool disposed = false;
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
    }
}
