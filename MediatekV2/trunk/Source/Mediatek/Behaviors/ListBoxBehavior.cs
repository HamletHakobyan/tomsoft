using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Developpez.Dotnet.Windows;

namespace Mediatek.Behaviors
{
    public static class ListBoxBehavior
    {
        public static ICommand GetItemDoubleClick(ListBox obj)
        {
            return (ICommand)obj.GetValue(ItemDoubleClickProperty);
        }

        public static void SetItemDoubleClick(ListBox obj, ICommand value)
        {
            obj.SetValue(ItemDoubleClickProperty, value);
        }

        // Using a DependencyProperty as the backing store for ItemDoubleClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemDoubleClickProperty =
            DependencyProperty.RegisterAttached(
              "ItemDoubleClick",
              typeof(ICommand),
              typeof(ListBoxBehavior),
              new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.Inherits,
                ItemDoubleClickChanged));

        private static void ItemDoubleClickChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var listBoxItem = o as ListBoxItem;
            if (listBoxItem == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                listBoxItem.RemoveHandler(
                    Control.MouseDoubleClickEvent,
                    (MouseButtonEventHandler)OnItemDoubleClick);
            }
            else if (newValue != null && oldValue == null)
            {
                listBoxItem.AddHandler(
                    Control.MouseDoubleClickEvent,
                    (MouseButtonEventHandler) OnItemDoubleClick);
            }
        }

        private static void OnItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;
            
            var listBoxItem = control as ListBoxItem ?? control.FindAncestor<ListBoxItem>();
            if (listBoxItem == null)
                return;
            
            var command = (ICommand) listBoxItem.GetValue(ItemDoubleClickProperty);
            if (command == null)
                return;

            var data = listBoxItem.DataContext;
            if (command.CanExecute(data))
                command.Execute(data);
        }

        public static bool GetScrollSelectedIntoView(ListBox obj)
        {
            return (bool)obj.GetValue(ScrollSelectedIntoViewProperty);
        }

        public static void SetScrollSelectedIntoView(ListBox obj, bool value)
        {
            obj.SetValue(ScrollSelectedIntoViewProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScrollSelectedIntoView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollSelectedIntoViewProperty =
            DependencyProperty.RegisterAttached(
              "ScrollSelectedIntoView",
              typeof(bool),
              typeof(ListBoxBehavior),
              new UIPropertyMetadata(
                false,
                ScrollSelectedIntoViewChanged));

        private static void ScrollSelectedIntoViewChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var listBox = o as ListBox;
            if (listBox == null)
                return;

            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                listBox.DoWhenLoaded(
                    () =>
                        {
                            if (listBox.SelectedItem != null)
                                listBox.ScrollIntoView(listBox.SelectedItem);
                        });
            }

        }

    }
}
