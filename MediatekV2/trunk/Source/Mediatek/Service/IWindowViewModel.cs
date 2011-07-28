using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mediatek.Service
{
    public interface IWindowViewModel
    {
        void OnClose();
        void OnClosing(CancelEventArgs e);
        event EventHandler<CloseRequestedEventArgs> CloseRequested;
    }

    public class CloseRequestedEventArgs : EventArgs
    {
        private readonly bool? _dialogResult;

        public CloseRequestedEventArgs(bool? dialogResult)
        {
            _dialogResult = dialogResult;
        }

        public bool? DialogResult
        {
            get { return _dialogResult; }
        }
    }
}
