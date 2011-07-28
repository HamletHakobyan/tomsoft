using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using Mediatek.Service;

namespace Mediatek
{
    public class MediatekWindow : Window
    {
        static MediatekWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MediatekWindow),
                new FrameworkPropertyMetadata(typeof(MediatekWindow)));
        }

        private ButtonBase _closeButton;
        private ButtonBase _maximizeButton;
        private ButtonBase _minimizeButton;
        private Control _titleBar;

        public MediatekWindow()
        {
            DataContextChanged += MediatekWindow_DataContextChanged;
        }

        void MediatekWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldVM = e.OldValue as IWindowViewModel;
            var newVM = e.NewValue as IWindowViewModel;
            if (oldVM != null)
            {
                oldVM.CloseRequested -= VM_CloseRequested;
            }
            if (newVM != null)
            {
                newVM.CloseRequested += VM_CloseRequested;
            }
        }

        void VM_CloseRequested(object sender, CloseRequestedEventArgs e)
        {
            CloseOverride(e);
        }

        protected virtual void CloseOverride(CloseRequestedEventArgs e)
        {
            if (ComponentDispatcher.IsThreadModal)
                DialogResult = e.DialogResult;
            else
                this.Close();
        }

        public object TitleBarContent
        {
            get { return GetValue(TitleBarContentProperty); }
            set { SetValue(TitleBarContentProperty, value); }
        }

        public static readonly DependencyProperty TitleBarContentProperty =
            DependencyProperty.Register("TitleBarContent", typeof(object), typeof(MediatekWindow), new UIPropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _closeButton = Template.FindName("PART_CloseButton", this) as ButtonBase;
            _maximizeButton = Template.FindName("PART_MaximizeButton", this) as ButtonBase;
            _minimizeButton = Template.FindName("PART_MinimizeButton", this) as ButtonBase;
            _titleBar = Template.FindName("PART_TitleBar", this) as Control;

            if (_closeButton != null)
                _closeButton.Click += CloseButton_Click;

            if (_maximizeButton != null)
                _maximizeButton.Click += MaximizeButton_Click;

            if (_minimizeButton != null)
                _minimizeButton.Click += MinimizeButton_Click;

            if (_titleBar != null)
                _titleBar.MouseDoubleClick += TitleBar_MouseDoubleClick;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleMaximize();
        }

        private void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ToggleMaximize();
            }
        }

        private void ToggleMaximize()
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            var viewModel = DataContext as IWindowViewModel;
            if (viewModel != null)
            {
                viewModel.OnClose();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            var viewModel = DataContext as IWindowViewModel;
            if (viewModel != null)
            {
                viewModel.OnClosing(e);
            }
        }
    }
}
