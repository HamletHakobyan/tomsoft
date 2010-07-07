using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.CSharp.RuntimeBinder;
using System.Windows.Controls.Primitives;

namespace SharpDB.Controls
{
    public class TabDocumentContainer : Selector
    {
        static TabDocumentContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabDocumentContainer), new FrameworkPropertyMetadata(typeof(TabDocumentContainer)));
        }

        #region Properties


        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(TabDocumentContainer), new UIPropertyMetadata(null));

        #endregion

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabDocumentContainerItem(this);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TabDocumentContainerItem;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                var containerItem = ItemContainerGenerator.ContainerFromItem(item) as TabDocumentContainerItem;
                if (containerItem != null)
                    containerItem.IsSelected = false;
            }

            foreach (var item in e.AddedItems)
            {
                var containerItem = ItemContainerGenerator.ContainerFromItem(item) as TabDocumentContainerItem;
                if (containerItem != null)
                    containerItem.IsSelected = true;
            }

            base.OnSelectionChanged(e);
        }

        internal void CloseTab(TabDocumentContainerItem tabDocumentContainerItem)
        {
            var item = ItemContainerGenerator.ItemFromContainer(tabDocumentContainerItem);
            
            // TODO find a better way...
            try
            {
                dynamic items = ItemsSource;
                items.Remove(item);
            }
            catch(RuntimeBinderException ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }
    }
}
