using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SharpDB.Util.Service
{
    public interface IMessageBoxService
    {
        MessageBoxResult Show(string messageBoxText);
        MessageBoxResult Show(string messageBoxText, string caption);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options);
    }
}
