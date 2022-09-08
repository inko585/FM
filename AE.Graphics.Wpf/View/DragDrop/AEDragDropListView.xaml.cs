using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für AEDragDropListView.xaml
    /// </summary>
    public partial class AEDragDropListView : ListView
    {
        // ---------- public commands ----------
        public static DependencyProperty DropCommandProperty = DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(AEDragDropListView), new PropertyMetadata(null));
        public ICommand DropCommand
        {
            get
            {
                return (ICommand)GetValue(DropCommandProperty);
            }

            set
            {
                SetValue(DropCommandProperty, value);
            }
        }


        // ---------- private fields  ----------
        private Type collectionType;

        // ---------- public constructor ----------
        public AEDragDropListView()
        {
            InitializeComponent();
        }

        // ---------- public methods ----------
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            if (this.ItemsSource != null)
            {
                this.collectionType = this.ItemsSource.GetType().GetGenericArguments().Single();
            }
        }

        // ---------- private methods ----------
        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null && e.LeftButton == MouseButtonState.Pressed && listView.SelectedItem != null)
            {
                DragDrop.DoDragDrop(listView, listView.SelectedItem, DragDropEffects.All);
            }

        }
        private void ListView_Drop(object sender, DragEventArgs e)
        {
            var dragItem = (e.Data as DataObject).GetData(collectionType);

            var hoverItem = (object)((FrameworkElement)e.OriginalSource).DataContext;
            var newItemIndex = GetIndexOfItemSourceCollection(hoverItem);

            if (this.DropCommand != null)
            {
                this.DropCommand.Execute(new DropItem() { NewIndex = newItemIndex, Item = dragItem });
            }
        }
        private int GetIndexOfItemSourceCollection(object item)
        {
            int i = 0;
            foreach (var itemSource in this.ItemsSource)
            {
                if (item == itemSource)
                    return i;

                i++;
            }

            return -1;
        }
    }
}
