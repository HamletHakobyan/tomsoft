using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Service;
using System.Windows;

namespace SharpDB.Service
{
    class BasicMessageBoxService : IMessageBoxService
    {
        public MessageBoxResult Show(string messageBoxText)
        {
            var owner = GetActiveWindow();
            return MessageBox.Show(owner, messageBoxText);
        }

        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            var owner = GetActiveWindow();
            return MessageBox.Show(owner, messageBoxText, caption);
        }

        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            var owner = GetActiveWindow();
            return MessageBox.Show(owner, messageBoxText, caption, button);
        }

        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var owner = GetActiveWindow();
            return MessageBox.Show(owner, messageBoxText, caption, button, icon);
        }

        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            var owner = GetActiveWindow();
            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult);
        }

        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            var owner = GetActiveWindow();
            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        private Window GetActiveWindow()
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                return null;

            return Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
        }
    }
}
