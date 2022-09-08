using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace AE.Graphics.Wpf
{
    public class AutoScrollListView : ListView
    {
        public bool IsItemVisible(int i)
        {
            if (Items.Count == 0)
            {
                return false;

            }
            var item = ItemContainerGenerator.ContainerFromItem(Items[i]) as ListViewItem;
            if (item == null)
            {
                return false;
            }
            var bounds = item.TransformToAncestor(this).TransformBounds(new System.Windows.Rect(0, 0, item.ActualWidth, item.ActualHeight));
            var rect = new System.Windows.Rect(0, 0, this.ActualWidth, this.ActualHeight);
            return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (!IsItemVisible(SelectedIndex))
            {
                ScrollIntoView(SelectedItem);
            }
        }
    }
}
