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

        public TabDocumentContainer()
        {
            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        void ItemContainerGenerator_StatusChanged(object sender, System.EventArgs e)
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                if (SelectedItem != null)
                {
                    var containerItem = ItemContainerGenerator.ContainerFromItem(SelectedItem) as TabDocumentContainerItem;
                    if (containerItem != null)
                        containerItem.IsSelected = true;
                }
                ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            }
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
        }

        internal void CloseTab(TabDocumentContainerItem tabDocumentContainerItem)
        {
            var index = SelectedIndex;
            var item = ItemContainerGenerator.ItemFromContainer(tabDocumentContainerItem);
            
            // TODO find a better way...
            try
            {
                dynamic items = ItemsSource;
                dynamic it = item;
                bool b = items.Remove(it);

                if (Items.Count > 0)
                {
                    if (index >= Items.Count)
                        index--;
                    if (index < 0)
                        index++;
                    SelectedIndex = index;
                }
            }
            catch(RuntimeBinderException ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }
    }
}
