using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.CSharp.RuntimeBinder;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System;
using SharpDB.Util;
using System.ComponentModel;

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



        public ICommand CloseTabCommand
        {
            get { return (ICommand)GetValue(CloseTabCommandProperty); }
            set { SetValue(CloseTabCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseTabCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseTabCommandProperty =
            DependencyProperty.Register("CloseTabCommand", typeof(ICommand), typeof(TabDocumentContainer), new UIPropertyMetadata(null));

        #endregion

        public event EventHandler<RequestCloseTabEventArgs> RequestCloseTab;
        public event EventHandler<TabClosingEventArgs> TabClosing;

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

            if (ItemsSource != null) // Databound
            {
                object item = ItemContainerGenerator.ItemFromContainer(tabDocumentContainerItem);
                if (item == null || item == DependencyProperty.UnsetValue)
                    return;

                if (RequestCloseTab != null)
                {
                    var args = new RequestCloseTabEventArgs(item);
                    RequestCloseTab(this, args);
                }
                else if (CloseTabCommand != null)
                {
                    if (CloseTabCommand.CanExecute(item))
                    {
                        CloseTabCommand.Execute(item);
                    }
                }
            }
            else // Not databound
            {
                if (TabClosing != null)
                {
                    var args = new TabClosingEventArgs(tabDocumentContainerItem);
                    TabClosing(this, args);
                    if (args.Cancel)
                        return;
                }
                Items.Remove(tabDocumentContainerItem);
            }


            if (Items.Count > 0)
            {
                if (index >= Items.Count)
                    index--;
                if (index < 0)
                    index++;
                SelectedIndex = index;
            }
        }
    }

    public class TabClosingEventArgs : CancelEventArgs
    {
        public TabClosingEventArgs(TabDocumentContainerItem tab)
        {
            this.Tab = tab;
        }

        public TabDocumentContainerItem Tab { get; set; }
    }

    public class RequestCloseTabEventArgs : EventArgs
    {
        public RequestCloseTabEventArgs(object dataItem)
        {
            this.DataItem = dataItem;
        }

        public object DataItem { get; private set; }
    }
}
