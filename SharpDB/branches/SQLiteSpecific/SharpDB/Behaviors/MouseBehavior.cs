using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace SharpDB.Behaviors
{
    public static class MouseBehavior
    {
        #region MouseDoubleClick

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseDoubleClick(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseDoubleClickProperty);
        }

        public static void SetMouseDoubleClick(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseDoubleClickProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseDoubleClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDoubleClickProperty =
            DependencyProperty.RegisterAttached(
                "MouseDoubleClick",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseDoubleClickChanged));

        private static void MouseDoubleClickChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseDoubleClick -= control_MouseDoubleClick;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseDoubleClick += control_MouseDoubleClick;
            }
        }

        static void control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseDoubleClick(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseDown

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseDownProperty);
        }

        public static void SetMouseDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseDownProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDownProperty =
            DependencyProperty.RegisterAttached(
                "MouseDown",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseDownChanged));

        private static void MouseDownChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseDown -= control_MouseDown;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseDown += control_MouseDown;
            }
        }

        static void control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseDown(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseEnter

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseEnter(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseEnterProperty);
        }

        public static void SetMouseEnter(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseEnterProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseEnter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseEnterProperty =
            DependencyProperty.RegisterAttached(
                "MouseEnter",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseEnterChanged));

        private static void MouseEnterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseEnter -= control_MouseEnter;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseEnter += control_MouseEnter;
            }
        }

        static void control_MouseEnter(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseEnter(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseLeave

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseLeave(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseLeaveProperty);
        }

        public static void SetMouseLeave(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseLeaveProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseLeave.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseLeaveProperty =
            DependencyProperty.RegisterAttached(
                "MouseLeave",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseLeaveChanged));

        private static void MouseLeaveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseLeave -= control_MouseLeave;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseLeave += control_MouseLeave;
            }
        }

        static void control_MouseLeave(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseLeave(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseLeftButtonDown

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseLeftButtonDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseLeftButtonDownProperty);
        }

        public static void SetMouseLeftButtonDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseLeftButtonDownProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseLeftButtonDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseLeftButtonDownProperty =
            DependencyProperty.RegisterAttached(
                "MouseLeftButtonDown",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseLeftButtonDownChanged));

        private static void MouseLeftButtonDownChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseLeftButtonDown -= control_MouseLeftButtonDown;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseLeftButtonDown += control_MouseLeftButtonDown;
            }
        }

        static void control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseLeftButtonDown(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseLeftButtonUp

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseLeftButtonUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseLeftButtonUpProperty);
        }

        public static void SetMouseLeftButtonUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseLeftButtonUpProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseLeftButtonUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseLeftButtonUpProperty =
            DependencyProperty.RegisterAttached(
                "MouseLeftButtonUp",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseLeftButtonUpChanged));

        private static void MouseLeftButtonUpChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseLeftButtonUp -= control_MouseLeftButtonUp;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseLeftButtonUp += control_MouseLeftButtonUp;
            }
        }

        static void control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseLeftButtonUp(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseMove

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseMove(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseMoveProperty);
        }

        public static void SetMouseMove(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseMoveProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseMove.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseMoveProperty =
            DependencyProperty.RegisterAttached(
                "MouseMove",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseMoveChanged));

        private static void MouseMoveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseMove -= control_MouseMove;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseMove += control_MouseMove;
            }
        }

        static void control_MouseMove(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseMove(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseRightButtonDown

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseRightButtonDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseRightButtonDownProperty);
        }

        public static void SetMouseRightButtonDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseRightButtonDownProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseRightButtonDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseRightButtonDownProperty =
            DependencyProperty.RegisterAttached(
                "MouseRightButtonDown",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseRightButtonDownChanged));

        private static void MouseRightButtonDownChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseRightButtonDown -= control_MouseRightButtonDown;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseRightButtonDown += control_MouseRightButtonDown;
            }
        }

        static void control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseRightButtonDown(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseRightButtonUp

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseRightButtonUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseRightButtonUpProperty);
        }

        public static void SetMouseRightButtonUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseRightButtonUpProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseRightButtonUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseRightButtonUpProperty =
            DependencyProperty.RegisterAttached(
                "MouseRightButtonUp",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseRightButtonUpChanged));

        private static void MouseRightButtonUpChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseRightButtonUp -= control_MouseRightButtonUp;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseRightButtonUp += control_MouseRightButtonUp;
            }
        }

        static void control_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseRightButtonUp(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseUp

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseUpProperty);
        }

        public static void SetMouseUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseUpProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseUpProperty =
            DependencyProperty.RegisterAttached(
                "MouseUp",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseUpChanged));

        private static void MouseUpChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseUp -= control_MouseUp;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseUp += control_MouseUp;
            }
        }

        static void control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseUp(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region MouseWheel

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetMouseWheel(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseWheelProperty);
        }

        public static void SetMouseWheel(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseWheelProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseWheel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseWheelProperty =
            DependencyProperty.RegisterAttached(
                "MouseWheel",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    MouseWheelChanged));

        private static void MouseWheelChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.MouseWheel -= control_MouseWheel;
            }

            if (newValue != null && oldValue == null)
            {
                control.MouseWheel += control_MouseWheel;
            }
        }

        static void control_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetMouseWheel(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseDoubleClick

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseDoubleClick(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseDoubleClickProperty);
        }

        public static void SetPreviewMouseDoubleClick(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseDoubleClickProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseDoubleClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseDoubleClickProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseDoubleClick",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseDoubleClickChanged));

        private static void PreviewMouseDoubleClickChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseDoubleClick -= control_PreviewMouseDoubleClick;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseDoubleClick += control_PreviewMouseDoubleClick;
            }
        }

        static void control_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseDoubleClick(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseDown

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseDownProperty);
        }

        public static void SetPreviewMouseDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseDownProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseDownProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseDown",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseDownChanged));

        private static void PreviewMouseDownChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseDown -= control_PreviewMouseDown;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseDown += control_PreviewMouseDown;
            }
        }

        static void control_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseDown(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseLeftButtonDown

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseLeftButtonDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseLeftButtonDownProperty);
        }

        public static void SetPreviewMouseLeftButtonDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseLeftButtonDownProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseLeftButtonDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseLeftButtonDownProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseLeftButtonDown",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseLeftButtonDownChanged));

        private static void PreviewMouseLeftButtonDownChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseLeftButtonDown -= control_PreviewMouseLeftButtonDown;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseLeftButtonDown += control_PreviewMouseLeftButtonDown;
            }
        }

        static void control_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseLeftButtonDown(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseLeftButtonUp

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseLeftButtonUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseLeftButtonUpProperty);
        }

        public static void SetPreviewMouseLeftButtonUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseLeftButtonUpProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseLeftButtonUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseLeftButtonUpProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseLeftButtonUp",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseLeftButtonUpChanged));

        private static void PreviewMouseLeftButtonUpChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseLeftButtonUp -= control_PreviewMouseLeftButtonUp;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseLeftButtonUp += control_PreviewMouseLeftButtonUp;
            }
        }

        static void control_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseLeftButtonUp(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseMove

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseMove(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseMoveProperty);
        }

        public static void SetPreviewMouseMove(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseMoveProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseMove.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseMoveProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseMove",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseMoveChanged));

        private static void PreviewMouseMoveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseMove -= control_PreviewMouseMove;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseMove += control_PreviewMouseMove;
            }
        }

        static void control_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseMove(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseRightButtonDown

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseRightButtonDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseRightButtonDownProperty);
        }

        public static void SetPreviewMouseRightButtonDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseRightButtonDownProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseRightButtonDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseRightButtonDownProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseRightButtonDown",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseRightButtonDownChanged));

        private static void PreviewMouseRightButtonDownChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseRightButtonDown -= control_PreviewMouseRightButtonDown;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseRightButtonDown += control_PreviewMouseRightButtonDown;
            }
        }

        static void control_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseRightButtonDown(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseRightButtonUp

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseRightButtonUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseRightButtonUpProperty);
        }

        public static void SetPreviewMouseRightButtonUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseRightButtonUpProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseRightButtonUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseRightButtonUpProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseRightButtonUp",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseRightButtonUpChanged));

        private static void PreviewMouseRightButtonUpChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseRightButtonUp -= control_PreviewMouseRightButtonUp;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseRightButtonUp += control_PreviewMouseRightButtonUp;
            }
        }

        static void control_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseRightButtonUp(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseUp

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseUpProperty);
        }

        public static void SetPreviewMouseUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseUpProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseUpProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseUp",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseUpChanged));

        private static void PreviewMouseUpChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseUp -= control_PreviewMouseUp;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseUp += control_PreviewMouseUp;
            }
        }

        static void control_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseUp(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

        #region PreviewMouseWheel

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static ICommand GetPreviewMouseWheel(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PreviewMouseWheelProperty);
        }

        public static void SetPreviewMouseWheel(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PreviewMouseWheelProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewMouseWheel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewMouseWheelProperty =
            DependencyProperty.RegisterAttached(
                "PreviewMouseWheel",
                typeof(ICommand),
                typeof(MouseBehavior),
                new UIPropertyMetadata(
                    null,
                    PreviewMouseWheelChanged));

        private static void PreviewMouseWheelChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var control = o as Control;
            if (control == null)
                return;

            var oldValue = (ICommand)e.OldValue;
            var newValue = (ICommand)e.NewValue;

            if (oldValue != null && newValue == null)
            {
                control.PreviewMouseWheel -= control_PreviewMouseWheel;
            }

            if (newValue != null && oldValue == null)
            {
                control.PreviewMouseWheel += control_PreviewMouseWheel;
            }
        }

        static void control_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var control = sender as Control;
            if (control == null)
                return;

            var command = GetPreviewMouseWheel(control);
            if (command != null)
            {
                if (command.CanExecute(e))
                    command.Execute(e);
            }
        }

        #endregion

    }
}
