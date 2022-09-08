using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AE.Graphics.Wpf.ViewModel
{
    public class AEResultViewModel : INotifyPropertyChanged
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
        private List<string> addedItems;
        private List<string> changedItems;
        private List<string> upToDateItems;

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
        public List<string> AddedItems
        {
            get { return this.addedItems; }
            set
            {
                if (this.addedItems == value)
                    return;

                this.addedItems = value;
                NotifyPropertyChanged("AddedItems");
            }
        }
        public List<string> ChangedItems
        {
            get { return this.changedItems; }
            set
            {
                if (this.changedItems == value)
                    return;

                this.changedItems = value;
                NotifyPropertyChanged("ChangedItems");
            }
        }
        public List<string> UpToDateItems
        {
            get { return this.upToDateItems; }
            set
            {
                if (this.upToDateItems == value)
                    return;

                this.upToDateItems = value;
                NotifyPropertyChanged("UpToDateItems");
            }
        }

        // ---------- public constructor ----------
        public AEResultViewModel(string title, List<string> addedItems = null, List<string> changedItems = null, List<string> upToDateItems = null)
        {
            this.title = title;

            this.AddedItems = addedItems;
            this.ChangedItems = changedItems;
            this.UpToDateItems = upToDateItems;
        }
    }
}
