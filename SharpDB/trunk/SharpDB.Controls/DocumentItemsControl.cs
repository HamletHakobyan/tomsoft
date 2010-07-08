using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;

namespace SharpDB.Controls
{
    class DocumentItemsControl : Selector
    {
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DocumentItem;
        }

        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return new DocumentItem();
        }

        protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                var documentItem = ItemContainerGenerator.ContainerFromItem(item) as DocumentItem;
                if (documentItem != null)
                    documentItem.IsSelected = false;
            }

            foreach (var item in e.AddedItems)
            {
                var documentItem = ItemContainerGenerator.ContainerFromItem(item) as DocumentItem;
                if (documentItem != null)
                    documentItem.IsSelected = true;
            }

            base.OnSelectionChanged(e);
        }
    }
}
