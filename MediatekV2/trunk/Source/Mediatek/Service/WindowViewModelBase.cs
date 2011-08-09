using System;
using System.ComponentModel;
using Developpez.Dotnet.Windows.ViewModel;

namespace Mediatek.Service
{
    public abstract class WindowViewModelBase : ViewModelBase, IWindowViewModel
    {
        void IWindowViewModel.OnShow()
        {
            OnShow();
        }

        protected virtual void OnShow()
        {
        }

        void IWindowViewModel.OnClose(bool? dialogResult)
        {
            OnClose(dialogResult);
        }

        protected virtual void OnClose(bool? dialogResult)
        {
        }

        void IWindowViewModel.OnClosing(bool? dialogResult, CancelEventArgs e)
        {
            OnClosing(dialogResult, e);
        }

        protected virtual void OnClosing(bool? dialogResult, CancelEventArgs e)
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