using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AE.Graphics.Wpf.Basis
{
    public static class RichListBox
    {
        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }

        public static List<A> GetDescendantsByType<A>(Visual element) where A : Visual
        {
            var ret = new List<A>();
            if (element == null)
            {
                return ret;
            }
            if (element.GetType() == typeof(A))
            {
                ret.Add((A)element);
            }

            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                ret.AddRange(GetDescendantsByType<A>(visual));
            }
            return ret;
        }

        public static void SetGroupExpanded(this ListBox lb, int no, bool expanded)
        {
            var sp = (StackPanel)GetDescendantByType(lb, typeof(StackPanel));
            if (sp != null)
            {
                _ = sp.Children[no] as GroupItem;
                var exp = (Expander)GetDescendantByType(lb, typeof(Expander));
                if (exp != null)
                {
                    exp.IsExpanded = expanded;
                }
            }

        }

        public static void SetGroupExpanded(this ListBox lb, string header, bool expanded)
        {
            var sp = (StackPanel)GetDescendantByType(lb, typeof(StackPanel));
            if (sp != null)
            {
                foreach (GroupItem gi in sp.Children)
                {
                    var exp = (Expander)GetDescendantByType(gi, typeof(Expander));
                    if (exp != null && ((string)((CollectionViewGroup)gi.Content).Name) == header)
                    {
                        exp.IsExpanded = expanded;
                    }

                }

            }

        }

        public static void SetGroupExpandend(this ListBox lb, ListBoxItem lbi, bool expanded)
        {
            var sp = (StackPanel)GetDescendantByType(lb, typeof(StackPanel));
            if (sp != null)
            {
                foreach (GroupItem gi in sp.Children)
                {
                    var exp = (Expander)GetDescendantByType(gi, typeof(Expander));
                    var content = GetDescendantsByType<ListBoxItem>(gi);
                    if (exp != null && content.Contains(lbi))
                    {
                        exp.IsExpanded = expanded;
                    }
                }


            }
        }

    }
}