using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AE.Graphics.Wpf.Basis
{
    public class AutoScroll : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject.Items.SourceCollection is INotifyCollectionChanged collection)
                collection.CollectionChanged += Collection_CollectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject.Items.SourceCollection is INotifyCollectionChanged collection)
                collection.CollectionChanged -= Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScrollToLastItem();
            }
        }

        private void ScrollToLastItem()
        {
            int count = AssociatedObject.Items.Count;
            if (count > 0)
            {
                var last = AssociatedObject.Items[count - 1];
                AssociatedObject.ScrollIntoView(last);
            }
        }
    }

    public class AutoScroll2Added : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject.Items.SourceCollection is INotifyCollectionChanged collection)
                collection.CollectionChanged += Collection_CollectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject.Items.SourceCollection is INotifyCollectionChanged collection)
                collection.CollectionChanged -= Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var addedIndex = e.NewStartingIndex;
                AssociatedObject.ScrollIntoView(AssociatedObject.Items[addedIndex]);
            }
        }

    }
}
