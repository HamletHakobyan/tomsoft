using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Developpez.Dotnet;

namespace SharpDB.Service
{
    class DialogServiceBase
    {
        [ThreadStatic]
        private static Stack<Window> _windows;
        protected Window GetTopWindow()
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

        protected IDisposable PushWindow(Window window)
        {
            _windows.Push(window);
            return new DisposableAction(() => _windows.Pop());
        }
    }
}
