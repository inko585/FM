using System;
using System.ComponentModel;
using System.Windows;

namespace AE.Graphics.Wpf.Basis
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // ---------- Close Request ----------
        public event EventHandler CloseRequest;
        protected void OnCloseRequest()
        {
            if (this.CloseRequest != null)
            {
                CloseRequest(this, EventArgs.Empty);
            }
        }

        // ---------- Interfaces ----------
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Window View { get; set; }
    }
}
