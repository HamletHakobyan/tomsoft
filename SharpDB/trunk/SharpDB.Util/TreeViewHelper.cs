using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SharpDB.Util
{
    public static class TreeViewHelper
    {
        public static object GetSelectedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewHelper), new UIPropertyMetadata(null));

        public static bool GetTrackSelection(DependencyObject obj)
        {
            return (bool)obj.GetValue(TrackSelectionProperty);
        }

        public static void SetTrackSelection(DependencyObject obj, bool value)
        {
            obj.SetValue(TrackSelectionProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrackSelection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackSelectionProperty =
            DependencyProperty.RegisterAttached(
              "TrackSelection",
              typeof(bool),
              typeof(TreeViewHelper),
              new UIPropertyMetadata(
                false,
                TrackSelectionChanged));

        private static void TrackSelectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var treeView = o as TreeView;
            if (treeView == null)
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                treeView.SelectedItemChanged -= treeView_SelectedItemChanged;
            }

            if (newValue && !oldValue)
            {
                treeView.SelectedItemChanged += treeView_SelectedItemChanged;
            }
        }

        static void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            if (treeView == null)
                return;
            treeView.SetCurrentValue(SelectedItemProperty, e.NewValue);
        }

    }
}
