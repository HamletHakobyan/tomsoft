using System;
using System.ComponentModel;
using Developpez.Dotnet.Windows.ViewModel;

namespace Mediatek.Service
{
    public abstract class WindowViewModelBase : ViewModelBase, IWindowViewModel
    {
        void IWindowViewModel.OnClose()
        {
            OnClose();
        }

        protected virtual void OnClose()
        {
        }

        void IWindowViewModel.OnClosing(CancelEventArgs e)
        {
            OnClosing(e);
        }

        protected virtual void OnClosing(CancelEventArgs cancelEventArgs)
        {
        }

        public event EventHandler<CloseRequestedEventArgs> CloseRequested;

        protected virtual void RequestClose(bool? dialogResult)
        {
            var handler = CloseRequested;
            if (handler != null)
                handler(this, new CloseRequestedEventArgs(dialogResult));
        }
    }
}