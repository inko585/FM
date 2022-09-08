using System;
using System.ComponentModel;
using System.IO;
using AE.Graphics.Wpf.Basis;
using Microsoft.Win32;

namespace AE.Graphics.Wpf.ViewModel
{
    public class AEFileSelectionViewModel : INotifyPropertyChanged
    {
        public event EventHandler CloseWindow;
        protected void OnCloseWindow()
        {
            if (CloseWindow != null)
                CloseWindow(this, EventArgs.Empty);
        }

        // ---------- private fields ----------
        private string title;
        private string selectionTitle;
        private string filePath;
        private readonly string regKeyCache = @"HKEY_CURRENT_USER\Software\NexansAE\AEWindow\FileSelectionCache";
        private readonly string openFileDialogFilter;

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
        public string SelectionTitle
        {
            get { return this.selectionTitle; }
            set
            {
                if (this.selectionTitle == value)
                    return;

                this.selectionTitle = value;
                NotifyPropertyChanged("SelectionTitle");
            }
        }
        public string FilePath
        {
            get { return this.filePath; }
            set
            {
                if (this.filePath == value)
                    return;

                this.filePath = value;
                NotifyPropertyChanged("FilePath");
            }
        }

        private string reloadToolTip;
        public string ReloadToolTip
        {
            get
            {
                return this.reloadToolTip;
            }
            set
            {
                this.reloadToolTip = value;
                NotifyPropertyChanged("ReloadToolTip");
            }
        }

        private bool reloadVisible;
        public bool ShowReload
        {
            get
            {
                return this.reloadVisible;
            }
            set
            {
                this.reloadVisible = value;
                NotifyPropertyChanged("ShowReload");
            }
        }

        private int txtSpan = 1;
        public int TxtSpan
        {
            get
            {
                return this.txtSpan;
            }
            set
            {
                this.txtSpan = value;
                NotifyPropertyChanged("TxtSpan");
            }
        }

        private string ReloadId { get; set; }

        private void ReloadFromCacheClicked()
        {
            if (ReloadId != null)
            {
                var file = (string)Registry.GetValue(regKeyCache, ReloadId + "_single", "");
                FilePath = file;

            }
        }

        public bool DialogResult { get; set; }

        // ---------- public constructor ----------
        public AEFileSelectionViewModel(string title, string selectionTitle, string filter, string reloadChacheId = null, string reloadCacheToolTip = "")
        {
            this.title = title;
            this.selectionTitle = selectionTitle;
            this.openFileDialogFilter = filter;

            InitializeButtons();
            ShowReload = reloadChacheId != null;
            TxtSpan = ShowReload ? 1 : 2;
            ReloadToolTip = reloadCacheToolTip;
            ReloadId = reloadChacheId;
        }

        // ---------- public methods ----------
        public void InitializeButtons()
        {
            this.OpenFileDialogCommand = new RelayCommand(e => OpenFileDialogClicked(), c => true);
            this.OkButtonCommand = new RelayCommand(e => OkButtonClicked(), c => true);
            this.ReloadLastCommand = new RelayCommand(e => ReloadFromCacheClicked(), c => true);
            this.CancelButtonCommand = new RelayCommand(e => CancelButtonClicked(), c => true);
        }

        private string BuildFilterString(string filter)
        {
            return filter + " files (*." + filter + ")|*." + filter;
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


        // ---------- public commands ----------
        public RelayCommand OpenFileDialogCommand { get; set; }
        public RelayCommand OkButtonCommand { get; set; }
        public RelayCommand CancelButtonCommand { get; set; }
        public RelayCommand ReloadLastCommand { get; set; }

        // ---------- private command methods ----------
        private void OpenFileDialogClicked()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter += "All files (*.*)|*.*";

            for (int i = 0; i < openFileDialogFilter.Length; i++)
            {
                openFileDialog.Filter += "|" + BuildFilterString(openFileDialogFilter);
            }

            if (openFileDialog.ShowDialog() == true)
                this.FilePath = openFileDialog.FileName;
        }
        private void OkButtonClicked()
        {
            this.DialogResult = true;
            if (ReloadId != null && File.Exists(FilePath))
            {
                Registry.SetValue(regKeyCache, ReloadId + "_single", FilePath);
            }
            OnCloseWindow();
        }
        private void CancelButtonClicked()
        {
            OnCloseWindow();
        }

        public void FileEnter(string f)
        {
            FilePath = f;
        }
    }
}