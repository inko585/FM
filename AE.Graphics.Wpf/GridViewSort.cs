using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AE.Graphics.Wpf
{


    public class GridViewSort
    {

        public static bool GetSortOnClick(DependencyObject obj)
        {
            return (bool)obj.GetValue(SortOnClick);
        }

        public static void SetSortOnClick(DependencyObject obj, bool value)
        {
            obj.SetValue(SortOnClick, value);
        }

        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(GridViewSort), new UIPropertyMetadata(null));

        public static readonly DependencyProperty SortOnClick = DependencyProperty.RegisterAttached("SortOnClick", typeof(bool), typeof(GridViewSort),
            new UIPropertyMetadata(false, (o, e) =>
                    {
                        if (o is ListView listView)
                        {
                            var oldValue = (bool)e.OldValue;
                            var newValue = (bool)e.NewValue;
                            if (oldValue && !newValue)
                            {
                                listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                            }
                            if (!oldValue && newValue)
                            {
                                listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                            }
                        }

                    }
                )
            );


        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader headerClicked)
            {
                string propertyName = GetPropertyName(headerClicked.Column);
                
                if (!string.IsNullOrEmpty(propertyName))
                {
                    var listView = AETreeHelper.GetAncestor<ListView>(headerClicked);
                    if (listView != null)
                    {
                        if (GetSortOnClick(listView))
                        {
                            ApplySort(listView.Items, propertyName);
                        }
                    }
                }
            }
        }

        public static void ApplySort(ICollectionView view, string propertyName)
        {
            var direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    direction = currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            }
        }



    }

    public class AETreeHelper
    {

        public static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
            {
                return (T)parent;
            }

            return null;
        }
    }
}
