using System;
using System.Linq;
using System.Windows.Controls;

namespace AE.Graphics.Wpf
{
    public class DragAndDropListView : ListView
    {
        public DragAndDropListView() => ItemsSource.GetType().GetGenericArguments().FirstOrDefault();
    }
}
