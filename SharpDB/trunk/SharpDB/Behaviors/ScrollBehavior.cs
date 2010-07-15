using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SharpDB.Util;

namespace SharpDB.Behaviors
{
    public static class ScrollBehavior
    {
        public static ICommand GetVerticalScrollCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(VerticalScrollCommandProperty);
        }

        public static void SetVerticalScrollCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(VerticalScrollCommandProperty, value);
        }

        public static readonly DependencyProperty VerticalScrollCommandProperty =
            DependencyProperty.RegisterAttached(
                "VerticalScrollCommand",
                typeof(ICommand),
                typeof(ScrollBehavior),
                new UIPropertyMetadata(
                    null,
                    VerticalScrollCommandChanged));



        public static ICommand GetHorizontalScrollCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(HorizontalScrollCommandProperty);
        }

        public static void SetHorizontalScrollCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(HorizontalScrollCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for HorizontalScrollCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollCommandProperty =
            DependencyProperty.RegisterAttached(
                "HorizontalScrollCommand",
                typeof(ICommand),
                typeof(ScrollBehavior),
                new UIPropertyMetadata(
                    null,
                    HorizontalScrollCommandChanged));



        private static void VerticalScrollCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = o as UIElement;
            if (uiElement == null)
                return;

            bool wasHooked = e.OldValue != null || GetHorizontalScrollCommand(o) != null;
            bool mustBeHooked = e.NewValue != null || GetHorizontalScrollCommand(o) != null;

            if (wasHooked && !mustBeHooked)
            {
                uiElement.RemoveHandler(RangeBase.ValueChangedEvent, (RoutedPropertyChangedEventHandler<double>)uiElement_ValueChanged);
            }

            if (mustBeHooked && !wasHooked)
            {
                uiElement.AddHandler(RangeBase.ValueChangedEvent, (RoutedPropertyChangedEventHandler<double>)uiElement_ValueChanged, true);
            }
        }

        private static void HorizontalScrollCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = o as UIElement;
            if (uiElement == null)
                return;

            bool wasHooked = e.OldValue != null || GetVerticalScrollCommand(o) != null;
            bool mustBeHooked = e.NewValue != null || GetVerticalScrollCommand(o) != null;

            if (wasHooked && !mustBeHooked)
            {
                uiElement.RemoveHandler(RangeBase.ValueChangedEvent, (RoutedPropertyChangedEventHandler<double>)uiElement_ValueChanged);
            }

            if (mustBeHooked && !wasHooked)
            {
                uiElement.AddHandler(RangeBase.ValueChangedEvent, (RoutedPropertyChangedEventHandler<double>)uiElement_ValueChanged, true);
            }
        }

        private static void uiElement_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var uiElement = sender as UIElement;
            if (uiElement == null)
                return;

            var sb = e.OriginalSource as ScrollBar;
            if (sb == null)
                return;

            var args = new ScrollValueChangedEventArgs(sb.Orientation, e.NewValue, e.OldValue, sb.Minimum, sb.Maximum);
            ICommand command;
            if (sb.Orientation == Orientation.Vertical)
                command = GetVerticalScrollCommand(uiElement);
            else
                command = GetHorizontalScrollCommand(uiElement);
            
            if (command != null && command.CanExecute(args))
                command.Execute(args);
        }
    }
}
