using System;
using System.ComponentModel;
using System.IO;
using AE.Graphics.Wpf.Basis;
using Microsoft.Win32;

namespace AE.Graphics.Wpf.ViewModel
{
    public class AE2FileSelectionViewModel : INotifyPropertyChanged
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

        private string firstFileNameLabel;
        private string filePathFirst;

        private string secondFileNameLabel;
        private string filePathSecond;
        private bool runTimeOut;
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

        
        public bool RunTimeOut
        {
            get
            {
                return runTimeOut;
            }
            
            set
            {
                runTimeOut = value;
                NotifyPropertyChanged("RunTimeOut");
            }
        }

        public string FirstFileNameLabel
        {
            get { return this.firstFileNameLabel; }
            set
            {
                if (this.firstFileNameLabel == value)
                    return;

                this.firstFileNameLabel = value;
                NotifyPropertyChanged("FirstFileNameLabel");
            }
        }
        public string FilePathFirst
        {
            get { return this.filePathFirst; }
            set
            {
                if (this.filePathFirst == value)
                    return;

                this.filePathFirst = value;
                NotifyPropertyChanged("FilePathFirst");
            }
        }


        public string SecondFileNameLabel
        {
            get { return this.secondFileNameLabel; }
            set
            {
                if (this.secondFileNameLabel == value)
                    return;

                this.secondFileNameLabel = value;
                NotifyPropertyChanged("SecondFileNameLabel");
            }
        }
        public string FilePathSecond
        {
            get { return this.filePathSecond; }
            set
            {
                if (this.filePathSecond == value)
                    return;

                this.filePathSecond = value;
                NotifyPropertyChanged("FilePathSecond");
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
            } set
            {
                this.txtSpan = value;
                NotifyPropertyChanged("TxtSpan");
            }
        }

        private string ReloadId { get; set; }

        // ---------- public constructor ----------
        public AE2FileSelectionViewModel(string title, string selectionTitle, string firstFileNameLabel, string secondFileNameLabel, string filter, string reloadChacheId = null, string reloadCacheToolTip = "")
        {
            this.title = title;
            this.selectionTitle = selectionTitle;

            this.firstFileNameLabel = firstFileNameLabel;
            this.secondFileNameLabel = secondFileNameLabel;

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
            this.OpenFileDialogFirstCommand = new RelayCommand(e => OpenFileDialogFirstClicked(), c => true);
            this.ReloadLastCommand = new RelayCommand(e => ReloadFromCacheClicked(), c => true);
            this.OpenFileDialogSecondCommand = new RelayCommand(e => OpenFileDialogSecondClicked(), c => true);
            this.OkButtonCommand = new RelayCommand(e => OkButtonClicked(), c => true);
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
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // ---------- public commands ----------
        public RelayCommand OpenFileDialogFirstCommand { get; set; }

        public RelayCommand OpenFileDialogSecondCommand { get; set; }

        public RelayCommand OkButtonCommand { get; set; }

        public RelayCommand CancelButtonCommand { get; set; }

        public RelayCommand ReloadLastCommand { get; set; }

        // ---------- private command methods ----------
        private void OpenFileDialogFirstClicked()
        {
            RunTimeOut = false;
            if (TryGetUserFileSelection(out string path))
            {
                FilePathFirst = path;
            }
            RunTimeOut = true;
        }

        private void OpenFileDialogSecondClicked()
        {
            RunTimeOut = false;
            if (TryGetUserFileSelection(out string path))
            {
                FilePathSecond = path;
            }
            RunTimeOut = true;
        }

        private bool TryGetUserFileSelection(out string path)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = openFileDialogFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                return true;
            }
            else
            {
                path = "";
                return false;
            }
        }
        private readonly string regKeyCache = @"HKEY_CURRENT_USER\Software\NexansAE\AEWindow\FileSelectionCache";

        private void ReloadFromCacheClicked()
        {
            if (ReloadId != null)
            {
                var fileA = (string)Registry.GetValue(regKeyCache, ReloadId + "_A", "");
                var fileB = (string)Registry.GetValue(regKeyCache, ReloadId + "_B", "");

                FilePathFirst = fileA;
                FilePathSecond = fileB;
            }
        }

        private void OkButtonClicked()
        {
            OnCloseWindow();
            if (ReloadId != null)
            {
                if (File.Exists(FilePathFirst))
                {
                    Registry.SetValue(regKeyCache, ReloadId + "_A", FilePathFirst);
                }
                if (File.Exists(FilePathSecond))
                {
                    Registry.SetValue(regKeyCache, ReloadId + "_B", filePathSecond);
                }
                
            }
        }
        private void CancelButtonClicked()
        {
            OnCloseWindow();
        }

        public void FileEnterA(string f)
        {
            FilePathFirst = f;
        }

        public void FileEnterB(string f)
        {
            FilePathSecond = f;
        }
    }
}