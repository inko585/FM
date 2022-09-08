using AE.Graphics.Wpf.Basis;
using System;
using System.ComponentModel;
using System.IO;

namespace AE.Graphics.Wpf.ViewModel
{
    public enum LinkType
    {
        Url,
        File,
        Program
    }

    public class AELinkViewModel : INotifyPropertyChanged
    {
        public event EventHandler CloseWindow;
        protected void OnCloseWindow()
        {
            if (CloseWindow != null)
                CloseWindow(this, EventArgs.Empty);
        }

        // ---------- INotifyPropertyChanged Interface ----------
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // ---------- private fields ----------
        private string title;
        private string message;
        private string linkText;
        private string link;

        private readonly LinkType linkType;

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
        public string LinkText
        {
            get { return this.linkText; }
            set
            {
                if (this.linkText == value)
                    return;

                this.linkText = value;
                NotifyPropertyChanged("LinkText");
            }
        }
        public string Link
        {
            get { return this.link; }
            set
            {
                if (this.link == value)
                    return;

                this.link = value;
                NotifyPropertyChanged("Link");
            }
        }

        public RelayCommand OpenLinkButtonCommand { get; set; }

        // ---------- public constructor ----------
        public AELinkViewModel(string title, string message, string linkText, string link, LinkType linkType)
        {
            this.title = title;
            this.message = message;
            this.linkText = linkText;
            this.link = link;
            this.linkType = linkType;

            this.OpenLinkButtonCommand = new RelayCommand(new Action<object>(OpenLinkExecute), c => true);
        }

        // ---------- private methods ----------
        internal void OpenLinkExecute(object obj)
        {
            if((this.linkType == LinkType.Program || this.linkType == LinkType.File) && (!File.Exists(this.link)))
            {
                AEGraphics.ShowError("The file: " + this.link + " doesn't exist!", "Error");
                return;
            }
            
            try
            {
                System.Diagnostics.Process.Start(this.link);
            }
            catch(Exception exp)
            {
                AEGraphics.ShowError(exp.Message, "Exception");
            }

            OnCloseWindow();
        }
    }
}