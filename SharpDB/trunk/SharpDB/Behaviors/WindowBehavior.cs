using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace SharpDB.Behaviors
{
    public static class WindowBehavior
    {
        public static ICommand GetClosing(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ClosingProperty);
        }

        public static void SetClosing(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClosingProperty, value);
        }

        // Using a DependencyProperty as the backing store for Closing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClosingProperty =
            DependencyProperty.RegisterAttached(
              "Closing",
              typeof(ICommand),
              typeof(WindowBehavior),
              new UIPropertyMetadata(
                null,
                ClosingChanged));

        private static void ClosingChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var window = o as Window;
            if (window == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {

            }
            else if (newValue != null && oldValue == null)
            {
                window.Closing += window_Closing;
            }
        }

        static void window_Closing(object sender, CancelEventArgs e)
        {
            var window = sender as Window;
            if (window == null)
                return;

            var command = GetClosing(window);
            if (command == null)
                return;

            if (command.CanExecute(e))
                command.Execute(e);
        }

    }
}
