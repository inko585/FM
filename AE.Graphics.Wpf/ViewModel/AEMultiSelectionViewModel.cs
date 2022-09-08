using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using AE.Graphics.Wpf.Basis;

namespace AE.Graphics.Wpf.ViewModel
{
    public class AEMultiSelectionViewModel<T>
    {
        public event EventHandler CloseWindow;
        protected void OnCloseWindow()
        {
            if (CloseWindow != null)
                CloseWindow(this, EventArgs.Empty);
        }

        private readonly string title;
        private readonly string selectionTitle;
        private readonly string selectAllText;
        private bool isSelectAll;
        private readonly Dictionary<string, T> selectionItems;
        private readonly Dictionary<CheckBox, T> boxes = new Dictionary<CheckBox, T>();


        public string Title => this.title;

        public string SelectionTitle => this.selectionTitle;

        public string SelectAllText => this.selectAllText;

        public bool SelectAllBox
        {
            get { return isSelectAll; }
            set
            {
                foreach (var box in boxes)
                {
                    box.Key.IsChecked = value;
                }
                isSelectAll = value;
            }
        }

        public List<CheckBox> SelectionItems => boxes.Keys.ToList();

        public List<T> SelectedItems => boxes.Where(x => x.Key.IsChecked.Value).Select(x => x.Value).ToList();

        // ---------- public constructor ----------
        public AEMultiSelectionViewModel(string title, string selectionTitle, string selectAllText, Dictionary<string, T> items)
        {
            this.title = title;
            this.selectionTitle = selectionTitle;
            this.selectionItems = items;
            this.selectAllText = selectAllText;

            foreach (var item in items.OrderBy(x => x.Key))
            {
                CheckBox cb = new CheckBox
                {
                    Content = "_" + item.Key, //first underscore functions as hotkey
                    Margin = new System.Windows.Thickness(5, 0, 0, 0)
                };
                boxes.Add(cb, item.Value);
            }
        }
    }
}