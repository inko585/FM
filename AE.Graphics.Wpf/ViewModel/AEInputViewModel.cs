using AE.Graphics.Wpf.Basis;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AE.Graphics.Wpf.ViewModel
{
    public enum InputType
    {
        Text,
        Number
    }

    public class AEInputViewModel : INotifyPropertyChanged
    {
        public event EventHandler CloseWindow;
        protected void OnCloseWindow()
        {
            if (CloseWindow != null)
                CloseWindow(this, EventArgs.Empty);
        }

        // ---------- private fields ----------
        private InputType inputType;
        private string title;
        private string inputTitle;
        private string input;
        private bool textInputGridVisible;
        private bool numericInputGridVisible;

        // ---------- public properties ----------
        public InputType InputType
        {
            get { return this.inputType; }
            set
            {
                if (this.inputType == value)
                    return;

                this.inputType = value;
                NotifyPropertyChanged("InputType");
            }
        }
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
        public string InputTitle
        {
            get { return this.inputTitle; }
            set
            {
                if (this.inputTitle == value)
                {
                    return;
                }

                this.inputTitle = value;
                NotifyPropertyChanged("InputTitle");
            }
        }
        public string Input
        {
            get { return this.input; }
            set
            {
                if (this.input == value)
                {
                    return;
                }

                if (inputType == InputType.Number)
                {
                    if (value == "")
                    {
                        this.Input = "0";
                    }
                    else
                    {
                        var regex = new Regex(@"^\-?\d+[,.]{0,1}\d*?$");
                        if (regex.IsMatch(value))
                        {
                            this.input = value.Replace(",", ".");
                        }
                    }
                }
                else
                {
                    this.input = value;
                }

                NotifyPropertyChanged("Input");
            }
        }
        public bool TextInputGridVisible
        {
            get { return this.textInputGridVisible; }
            set
            {
                if (this.textInputGridVisible == value)
                    return;

                this.textInputGridVisible = value;
                NotifyPropertyChanged("TextInputGridVisible");
            }
        }
        public bool NumericInputGridVisible
        {
            get { return this.numericInputGridVisible; }
            set
            {
                if (this.numericInputGridVisible == value)
                    return;

                this.numericInputGridVisible = value;
                NotifyPropertyChanged("NumericInputGridVisible");
            }
        }

        // ---------- public constructor ----------
        public AEInputViewModel(string title, string inputTitle, InputType inputType)
        {
            this.title = title;
            this.inputTitle = inputTitle;
            this.inputType = inputType;

            this.OkButtonCommand = new RelayCommand(e => OkButtonClicked(), c => true);
            this.CancelButtonCommand = new RelayCommand(e => CancelButtonClicked(), c => true);
            this.NumericUpCommand = new RelayCommand(e => NumericUp(), c => true);
            this.NumericDownCommand = new RelayCommand(e => NumericDown(), c => true);

            InitializeInputType();
        }

        // ---------- public methods ----------
        public void InitializeInputType()
        {
            if (this.inputType == InputType.Text)
            {
                this.TextInputGridVisible = true;
                this.NumericInputGridVisible = false;

                this.Input = string.Empty;
            }
            else if (this.inputType == InputType.Number)
            {
                this.TextInputGridVisible = false;
                this.NumericInputGridVisible = true;

                this.Input = 0.ToString();
            }
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
        public bool DialogResult { get; set; }

        public RelayCommand OkButtonCommand { get; set; }
        public RelayCommand CancelButtonCommand { get; set; }
        public RelayCommand NumericUpCommand { get; set; }
        public RelayCommand NumericDownCommand { get; set; }

        // ---------- private command methods ----------
        private void OkButtonClicked()
        {
            this.DialogResult = true;

            if ((this.Input != null && this.Input.ToString() != "") && (this.inputType == InputType.Number) && (!decimal.TryParse(this.Input.ToString(), out decimal numberResult)))
            {
                AEGraphics.ShowWarning("Wrong Number Format!", "Format");
                this.DialogResult = false;
            }

            if (this.DialogResult)
            {
                OnCloseWindow();
            }
        }
        private void CancelButtonClicked()
        {
            this.DialogResult = false;
            OnCloseWindow();
        }
        private void NumericUp()
        {
            this.Input = (Decimal.Parse(this.Input, CultureInfo.InvariantCulture) + 1).ToString(CultureInfo.InvariantCulture);
        }
        private void NumericDown()
        {
            this.Input = (Decimal.Parse(this.Input, CultureInfo.InvariantCulture) - 1).ToString(CultureInfo.InvariantCulture);
        }
    }
}