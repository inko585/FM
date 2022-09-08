using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using AE.Graphics.Wpf.Basis;

namespace AE.Graphics.Wpf.ViewModel
{
    public class AESelectionViewModel<T> : INotifyPropertyChanged
    {
        public event EventHandler CloseWindow;
        protected void OnCloseWindow()
        {
            if (CloseWindow != null)
                CloseWindow(this, EventArgs.Empty);
        }

        private string title;
        private string selectionTitle;
        private readonly Dictionary<string, T> selectionItems;
        private List<RadioButton> selectedItemsRadioButtons;
        private RadioButton checkedRadioButton;

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
        public List<RadioButton> SelectionItems
        {
            get
            {
                return selectedItemsRadioButtons;
            }

            set
            {
                if (selectedItemsRadioButtons == value)
                    return;

                selectedItemsRadioButtons = value;
                NotifyPropertyChanged("SelectionItems");
            }
        }
        public T SelectedItem
        {
            get
            {
                if (checkedRadioButton != null)
                {
                    T selectedItem = this.selectionItems[checkedRadioButton.Content.ToString()];

                    if (selectedItem != null)
                    { return selectedItem; }
                    else
                    { return default; }

                }
                else
                { return default; }
            }
        }

        // ---------- public constructor ----------
        public AESelectionViewModel(string title, string selectionTitle, Dictionary<string, T> items)
        {
            this.title = title;
            this.selectionTitle = selectionTitle;

            this.selectionItems = items;

            this.selectedItemsRadioButtons = new List<RadioButton>();

            foreach (string itemDesc in items.Keys)
            {
                RadioButton nRadioButton = new RadioButton
                {
                    Content = itemDesc,
                    GroupName = "RadioButtonGroup",
                    Margin = new System.Windows.Thickness(5, 0, 0, 0)
                };
                nRadioButton.Checked += ItemSelectionChecked;

                selectedItemsRadioButtons.Add(nRadioButton);
            }

            this.OkButtonCommand = new RelayCommand(a => OkButtonClicked(), c => true);
        }

        // ---------- private methods ----------
        private void ItemSelectionChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.checkedRadioButton = sender as RadioButton;
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
        public RelayCommand OkButtonCommand { get; set; }

        // ---------- private command methods ----------
        private void OkButtonClicked()
        {
            OnCloseWindow();
        }
    }
}