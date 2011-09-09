using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

namespace SharpDB.Behaviors
{
    public static class RichTextBoxBehavior
    {
        public static FlowDocument GetDocument(DependencyObject obj)
        {
            return (FlowDocument)obj.GetValue(DocumentProperty);
        }

        public static void SetDocument(DependencyObject obj, FlowDocument value)
        {
            obj.SetValue(DocumentProperty, value);
        }

        // Using a DependencyProperty as the backing store for Document.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.RegisterAttached(
              "Document",
              typeof(FlowDocument),
              typeof(RichTextBoxBehavior),
              new UIPropertyMetadata(
                null,
                DocumentChanged));

        private static void DocumentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var rtb = o as RichTextBox;
            if (rtb == null)
                return;

            var newValue = (FlowDocument)e.NewValue;

            if (rtb.Document != newValue)
                rtb.Document = newValue;
        }

        public static bool GetAutoScrollToEnd(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToEndProperty);
        }

        public static void SetAutoScrollToEnd(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToEndProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoScrollToEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoScrollToEndProperty =
            DependencyProperty.RegisterAttached(
              "AutoScrollToEnd",
              typeof(bool),
              typeof(RichTextBoxBehavior),
              new UIPropertyMetadata(
                false,
                AutoScrollToEndChanged));

        private static void AutoScrollToEndChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var rtb = o as RichTextBox;
            if (rtb == null)
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                rtb.TextChanged -= rtb_TextChanged;
            }
            else if (newValue && !oldValue)
            {
                rtb.TextChanged += rtb_TextChanged;
            }
        }

        static void rtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var rtb = sender as RichTextBox;
            if (rtb == null)
                return;

            rtb.ScrollToEnd();
        }

    }
}
