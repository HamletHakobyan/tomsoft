using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace VBulletinBox.Behaviors
{
    public static class PasswordBoxBehavior
    {
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static bool GetBindPassword(PasswordBox obj)
        {
            return (bool)obj.GetValue(BindPasswordProperty);
        }

        public static void SetBindPassword(PasswordBox obj, bool value)
        {
            obj.SetValue(BindPasswordProperty, value);
        }

        public static readonly DependencyProperty BindPasswordProperty =
            DependencyProperty.RegisterAttached(
              "BindPassword",
              typeof(bool),
              typeof(PasswordBoxBehavior),
              new UIPropertyMetadata(
                false,
                BindPasswordChanged));

        private static void BindPasswordChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var pb = o as PasswordBox;
            if (pb == null)
                return;
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                pb.PasswordChanged -= pb_PasswordChanged;
            }
            else if (newValue && !oldValue)
            {
                pb.PasswordChanged += pb_PasswordChanged;
            }
        }

        static void pb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null || !GetBindPassword(pb))
                return;
            if (GetPassword(pb) != pb.Password)
                SetPassword(pb, pb.Password);
        }

        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static string GetPassword(PasswordBox obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(PasswordBox obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
              "Password",
              typeof(string),
              typeof(PasswordBoxBehavior),
              new UIPropertyMetadata(
                null,
                PasswordChanged));

        private static void PasswordChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var pb = o as PasswordBox;
            if (pb == null || !GetBindPassword(pb))
                return;

            var newValue = (string)e.NewValue;
            if (pb.Password != newValue)
                pb.Password = newValue;
        }

    }
}
