using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Dialogs;
using System.Windows;

namespace SharpDB.Service
{
    class DialogService : IDialogService
    {
        [ThreadStatic]
        private static Stack<Window> _windows;

        public bool? Show(IDialogViewModel viewModel)
        {
            var window = new DialogWindow();
            window.Owner = GetTopWindow();
            _windows.Push(window);
            try
            {
                return window.Show(viewModel);
            }
            finally
            {
                _windows.Pop();
            }
            
        }

        private Window GetTopWindow()
        {
            if (_windows == null)
            {
                _windows = new Stack<Window>();
                if (Application.Current.Dispatcher.CheckAccess())
                    _windows.Push(Application.Current.MainWindow);
            }
            
            if (_windows.Count > 0)
                return _windows.Peek();
            
            return null;
        }
    }
}
