using AE.Graphics.Wpf.Basis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace AE.Graphics.Wpf.ViewModel
{
    class AEMultipleInputViewModel : INotifyPropertyChanged
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
        private string inputTitle;
        private readonly List<Label> labels;
        private readonly List<TextBox> textBoxes;

        private List<Grid> inputControls;

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
        public string InputTitle
        {
            get { return this.inputTitle; }
            set
            {
                if (this.inputTitle == value)
                    return;

                this.inputTitle = value;
                NotifyPropertyChanged("InputTitle");
            }
        }

        public List<Grid> InputControls
        {
            get { return this.inputControls; }
            set
            {
                if (this.inputControls == value)
                    return;

                this.inputControls = value;
                NotifyPropertyChanged("InputControls");
            }
        }

        public RelayCommand OkButtonCommand { get; set; }
        public RelayCommand CancelButtonCommand { get; set; }

        public bool DialogResult { get; set; }
        public Dictionary<string, string> Result;

        // ---------- public constructor ----------
        public AEMultipleInputViewModel(string title, string inputTitle, params string[] labels)
        {
            this.OkButtonCommand = new RelayCommand(new Action<object>(OkButtonExecute), c => true);
            this.CancelButtonCommand = new RelayCommand(new Action<object>(CancelButtonExecute), c => true);

            this.Title = title;
            this.InputTitle = inputTitle;

            this.labels = new List<Label>();
            this.textBoxes = new List<TextBox>();
            this.inputControls = new List<Grid>();

            this.Result = new Dictionary<string, string>();

            CreateInputControls(labels);
        }

        // ---------- private methods ----------
        private void CreateInputControls(string[] labels)
        {
            foreach(string label in labels)
            {
                Grid nGrid = CreateGrid();

                CreateAndAddLabel(label, nGrid);
                CreateAndAddTextBox(label, nGrid);

                this.InputControls.Add(nGrid);
            }
        }
        private Grid CreateGrid()
        {
            Grid nGrid = new Grid();

            nGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(100) });
            nGrid.ColumnDefinitions.Add(new ColumnDefinition());

            return nGrid;
        }
        private void CreateAndAddLabel(string labelName, Grid grid)
        {
            Label nLabel = new Label()
            {
                Content = labelName,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };
            Grid.SetColumn(nLabel, 0);

            grid.Children.Add(nLabel);
            this.labels.Add(nLabel);
        }
        private void CreateAndAddTextBox(string labelName, Grid grid)
        {
            TextBox nTextBox = new TextBox()
            {
                Name = labelName.Replace(" ", ""),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new System.Windows.Thickness(5, 0, 5, 0)
            };
            Grid.SetColumn(nTextBox, 1);

            grid.Children.Add(nTextBox);
            this.textBoxes.Add(nTextBox);
        }
        private bool AllInputControlsFilled()
        {
            foreach(TextBox textBox in this.textBoxes)
            {
                if (textBox.Text == string.Empty)
                    return false;
            }
            return true;
        }
        private void BuildResult()
        {
            foreach(TextBox textBox in this.textBoxes)
            {
                this.Result.Add(textBox.Name, textBox.Text);
            }
        }

        private void OkButtonExecute(object obj)
        {
            if(AllInputControlsFilled())
            {
                this.DialogResult = true;
                BuildResult();

                OnCloseWindow();
            }
            else
            {
                AEGraphics.ShowWarning("Please fill out all input controls!", "Multiple Input");
            }
        }
        private void CancelButtonExecute(object obj)
        {
            this.DialogResult = false;
            OnCloseWindow();
        }
    }
}