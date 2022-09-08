using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für AENotificationView.xaml
    /// </summary>
    public partial class AENotificationView : Window
    {
        public enum NotificationType { Success, Warning, Error }

        public AENotificationView(NotificationType nt, string message)
            : this(Application.Current.MainWindow, nt, message)
        {
        }

        public void AdjustPosition()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var presentationSource = PresentationSource.FromVisual(this);
                if (presentationSource != null)
                {
                    var transform = presentationSource.CompositionTarget.TransformFromDevice;

                    var corner = transform.Transform(new Point(window.Left, window.Top));

                    this.Width = Math.Max(0, window.ActualWidth - 30 - freeSpace);
                    this.Left = corner.X + 15;
                    this.Top = corner.Y + window.ActualHeight - (this.ActualHeight + 15);

                    if (window.WindowState == WindowState.Maximized)
                    {
                        var leftField = typeof(Window).GetField("_actualLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        this.Left = (double)leftField.GetValue(window) + 15;
                        this.Top = window.ActualHeight - (this.ActualHeight + 15);
                    }
                }
            }));
        }


        private static Dictionary<Window, AENotificationView> CurrentNotification = new Dictionary<Window, AENotificationView>();
        private int freeSpace = 0;
        private Window window;

        private EventHandler stateHandler;
        private EventHandler closeHandler;
        private EventHandler locationHandler;
        private SizeChangedEventHandler sizeHandler;

        public AENotificationView(Window window, NotificationType nt, string message, int freeSpace = 0)
        {
            InitializeComponent();
            this.freeSpace = freeSpace;
            this.window = window;

            AENotificationView current;
            if (CurrentNotification.TryGetValue(window, out current))
            {
                current.Close();
            }

            CurrentNotification[window] = this;

            stateHandler = new EventHandler( (o, e) =>
            {
                if (window.WindowState == WindowState.Minimized)
                {
                    Visibility = Visibility.Hidden;
                }
                 else if (Visibility == Visibility.Hidden)
                {
                    Visibility = Visibility.Visible;
                }

            });
            
            locationHandler = (o, e) => AdjustPosition();
            sizeHandler = (o, e) => AdjustPosition();
            closeHandler = (o, e) => Close();

            window.LocationChanged += locationHandler;
            window.SizeChanged += sizeHandler;
            window.Closed += closeHandler;
            window.StateChanged += stateHandler;
            Owner = window;

            txtMessage.Text = message;

            BrushConverter bc = new BrushConverter();
            Brush brushSuccess = (Brush)bc.ConvertFrom("#51a351");
            Brush brushWarning = (Brush)bc.ConvertFrom("#f89406");
            Brush brushError = (Brush)bc.ConvertFrom("#bd362f");

            switch (nt)
            {
                case NotificationType.Success:
                    borderWindow.Background = brushSuccess;
                    break;
                case NotificationType.Warning:
                    borderWindow.Background = brushWarning;
                    break;
                case NotificationType.Error:
                    borderWindow.Background = brushError;
                    break;
                default:
                    break;
            }

            AdjustPosition();

        }

        protected override void OnClosed(EventArgs e)
        {            
            window.StateChanged -= stateHandler;
            window.LocationChanged -= locationHandler;
            window.SizeChanged -= sizeHandler;
        }

        private void DoubleAnimationUsingKeyFrames_Completed(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Storyboard)FindResource("fadeout")).Begin(this);
        }


    }
}
