using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mediatek.Behaviors
{
    public static class ButtonStateBehavior
    {
        public static bool GetTrackState(DependencyObject obj)
        {
            return (bool)obj.GetValue(TrackStateProperty);
        }

        public static void SetTrackState(DependencyObject obj, bool value)
        {
            obj.SetValue(TrackStateProperty, value);
        }

        public static readonly DependencyProperty TrackStateProperty =
            DependencyProperty.RegisterAttached(
              "TrackState",
              typeof(bool),
              typeof(ButtonStateBehavior),
              new UIPropertyMetadata(
                false,
                TrackStateChanged));

        private static void TrackStateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var button = o as ButtonBase;
            if (button == null)
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                RemovePropertyChangedHandler(button, IsMouseOverPropertyKey.DependencyProperty, button_IsMouseOverChanged);
                RemovePropertyChangedHandler(button, IsPressedPropertyKey.DependencyProperty, button_IsPressedChanged);
            }
            else if (newValue && !oldValue)
            {
                AddPropertyChangedHandler(button, UIElement.IsMouseOverProperty, button_IsMouseOverChanged);
                AddPropertyChangedHandler(button, ButtonBase.IsPressedProperty, button_IsPressedChanged);
            }
        }

        private static void AddPropertyChangedHandler(DependencyObject obj, DependencyProperty property, EventHandler handler)
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(property, obj.GetType());
            descriptor.AddValueChanged(obj, handler);
        }

        private static void RemovePropertyChangedHandler(DependencyObject obj, DependencyProperty property, EventHandler handler)
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(property, obj.GetType());
            descriptor.RemoveValueChanged(obj, handler);
        }

        private static void button_IsMouseOverChanged(object sender, EventArgs e)
        {
            var button = sender as ButtonBase;
            if (button == null)
                return;

            button.SetValue(ButtonStateBehavior.IsMouseOverPropertyKey, button.IsMouseOver);
        }

        private static void button_IsPressedChanged(object sender, EventArgs e)
        {
            var button = sender as ButtonBase;
            if (button == null)
                return;

            button.SetValue(ButtonStateBehavior.IsPressedPropertyKey, button.IsPressed);
        }

        private static readonly DependencyPropertyKey IsMouseOverPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsMouseOver",
                typeof(bool),
                typeof(ButtonStateBehavior),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty IsMouseOverProperty = IsMouseOverPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey IsPressedPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsPressed",
                typeof(bool),
                typeof(ButtonStateBehavior),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty IsPressedProperty = IsPressedPropertyKey.DependencyProperty;

        public static bool GetIsMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseOverPropertyKey.DependencyProperty);
        }

        public static bool GetIsPressed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPressedPropertyKey.DependencyProperty);
        }
    }
}
