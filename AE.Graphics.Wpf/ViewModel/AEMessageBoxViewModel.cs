using System;
using System.ComponentModel;
using System.Windows.Media;
using AE.Graphics.Wpf.Basis;
using AE.Graphics.Wpf.View;

namespace AE.Graphics.Wpf.ViewModel
{
    public enum MessageBoxMode
    {
        Message,
        DontShowMeAgainMessage,
        Ask,
        DontShowMeAgainAsk,
    }

    public class AEMessageBoxViewModel : INotifyPropertyChanged
    {
        public event EventHandler CloseWindow;
        protected void OnCloseWindow()
        {
            if (CloseWindow != null)
                CloseWindow(this, EventArgs.Empty);
        }

        // ---------- private fields ----------
        private readonly MessageBoxMode mode;
        private string title;
        private string icon;
        private Brush iconColor;
        private string message;
        private string okButtonText;
        private string yesButtonText;
        private string noButtonText;
        private bool iconVisible;
        private bool okButtonVisible;
        private bool yesButtonVisible;
        private bool noButtonVisible;

        private bool isDontShowMeAgain;
        private bool dontShowMeAgain;

        // ---------- public properties ----------
        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title == value)
                    return;

                this.title = value;
                NotifyPropertyChanged("Title");
            }
        }
        private bool timeOutActive;

        public bool TimeOutActive
        {
            get { return timeOutActive; }
            set
            {
                timeOutActive = value;
                NotifyPropertyChanged("TimeOutActive");
            }
        }

        private int timeOutLength;

        public int TimeOutLength
        {
            get { return timeOutLength; }
            set
            {
                timeOutLength = value;
                NotifyPropertyChanged("TimeOutLength");
            }

        }

        private TimeOutAction timeOutAction;

        public TimeOutAction TimeOutAction
        {
            get { return timeOutAction; }
            set
            {
                timeOutAction = value;
                NotifyPropertyChanged("TimeOutAction");
            }
        }


        public string Icon
        {
            get { return this.icon; }
            set
            {
                if (this.icon == value)
                    return;

                this.icon = value;
                NotifyPropertyChanged("Icon");
            }
        }
        public Brush IconColor
        {
            get { return this.iconColor; }
            set
            {
                if (this.iconColor == value)
                    return;

                this.iconColor = value;
                NotifyPropertyChanged("IconColor");
            }
        }
        public string Message
        {
            get { return this.message; }
            set
            {
                if (this.message == value)
                    return;

                this.message = value;
                NotifyPropertyChanged("Message");
            }
        }
        public string OkButtonText
        {
            get { return this.okButtonText; }
            set
            {
                if (this.okButtonText == value)
                    return;

                this.okButtonText = value;
                NotifyPropertyChanged("OkButtonText");
            }
        }
        public string YesButtonText
        {
            get { return this.yesButtonText; }
            set
            {
                if (this.yesButtonText == value)
                    return;

                this.yesButtonText = value;
                NotifyPropertyChanged("YesButtonText");
            }
        }
        public string NoButtonText
        {
            get { return this.noButtonText; }
            set
            {
                if (this.noButtonText == value)
                    return;

                this.noButtonText = value;
                NotifyPropertyChanged("NoButtonText");
            }
        }
        public bool IconVisible
        {
            get { return this.iconVisible; }
            set
            {
                if (this.iconVisible == value)
                    return;

                this.iconVisible = value;
                NotifyPropertyChanged("IconVisible");
            }
        }
        public bool OkButtonVisible
        {
            get { return this.okButtonVisible; }
            set
            {
                if (this.okButtonVisible == value)
                    return;

                this.okButtonVisible = value;
                NotifyPropertyChanged("OkButtonVisible");
            }
        }
        public bool YesButtonVisible
        {
            get { return this.yesButtonVisible; }
            set
            {
                if (this.yesButtonVisible == value)
                    return;

                this.yesButtonVisible = value;
                NotifyPropertyChanged("YesButtonVisible");
            }
        }
        public bool NoButtonVisible
        {
            get { return this.noButtonVisible; }
            set
            {
                if (this.noButtonVisible == value)
                    return;

                this.noButtonVisible = value;
                NotifyPropertyChanged("NoButtonVisible");
            }
        }
        public bool IsDontShowMeAgain
        {
            get { return this.isDontShowMeAgain; }
            set
            {
                if (this.isDontShowMeAgain == value)
                    return;

                this.isDontShowMeAgain = value;
                NotifyPropertyChanged("IsDontShowMeAgain");
            }
        }
        public bool DontShowMeAgain
        {
            get { return this.dontShowMeAgain; }
            set
            {
                if (this.dontShowMeAgain == value)
                    return;

                this.dontShowMeAgain = value;
                NotifyPropertyChanged("DontShowMeAgain");
            }
        }

        public bool DialogResult { get; set; }

        // ---------- public constructor ----------
        public AEMessageBoxViewModel(string message, string title, MessageBoxIcon icon, MessageBoxMode mode, string yesButtonText = "Yes", string noButtonText = "No", int timeOutLength = -1, Action onTimeOut = null)
        {
            this.mode = mode;
            this.YesButtonText = yesButtonText;
            this.NoButtonText = noButtonText;
            if (timeOutLength > -1)
            {
                TimeOutLength = timeOutLength;
                TimeOutActive = true;
                if (onTimeOut != null)
                {
                    TimeOutAction = new TimeOutAction(true, onTimeOut);
                }
            }
            else
            {
                TimeOutActive = false;
            }

            if (this.mode == MessageBoxMode.DontShowMeAgainMessage || this.mode == MessageBoxMode.DontShowMeAgainAsk)
            {
                this.IsDontShowMeAgain = true;
            }
            else
            {
                this.IsDontShowMeAgain = false;
            }

            this.Message = BuildMessage(message);
            this.Title = title;

            InitializeButtons();
            InitializeIcon(icon);
        }

        // ---------- INotifyPropertyChanged Interface ----------
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // ---------- privat methods ----------
        private void InitializeButtons()
        {
            if (this.mode == MessageBoxMode.Message || this.mode == MessageBoxMode.DontShowMeAgainMessage)
            {
                this.OkButtonVisible = true;
                this.YesButtonVisible = false;
                this.NoButtonVisible = false;
            }
            else
            {
                this.OkButtonVisible = false;
                this.YesButtonVisible = true;
                this.NoButtonVisible = true;
            }

            this.OkButtonText = "Ok";

            this.OkButtonCommand = new RelayCommand(a => OkButtonClicked(), c => true);
            this.YesButtonCommand = new RelayCommand(a => YesButtonClicked(), c => true);
            this.NoButtonCommand = new RelayCommand(a => NoButtonClicked(), c => true);
        }

        private void InitializeIcon(MessageBoxIcon icon)
        {
            this.IconVisible = true;

            if (icon == MessageBoxIcon.Question)
            {
                this.Icon = "\uf059";
                this.IconColor = Brushes.CornflowerBlue;
            }

            else if (icon == MessageBoxIcon.Information)
            {
                this.Icon = "\uf129";
                this.IconColor = Brushes.CornflowerBlue;
            }

            else if (icon == MessageBoxIcon.Warning)
            {
                this.Icon = "\uf071";
                this.IconColor = Brushes.Goldenrod;
            }

            else if (icon == MessageBoxIcon.Error)
            {
                this.Icon = "\uf057";
                this.IconColor = Brushes.Tomato;
            }

            else if (icon == MessageBoxIcon.Default)
            {
                this.IconVisible = false;
            }
        }

        protected string BuildMessage(string message)
        {


            return message;
        }

        // ---------- public commands ----------
        public RelayCommand OkButtonCommand { get; set; }
        public RelayCommand YesButtonCommand { get; set; }
        public RelayCommand NoButtonCommand { get; set; }

        // ---------- private command methods ----------
        private void OkButtonClicked()
        {
            this.DialogResult = true;
            OnCloseWindow();
        }
#pragma warning disable S4144
        private void YesButtonClicked()
#pragma warning restore S4144 
        {
            this.DialogResult = true;
            OnCloseWindow();
        }
        private void NoButtonClicked()
        {
            this.DialogResult = false;
            OnCloseWindow();
        }
    }
}