using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Mediatek.Service.Implementation
{
    public class DialogService : IDialogService
    {
        private readonly Stack<Window> _owners = new Stack<Window>();

        public DialogService(Window rootWindow)
        {
            if (rootWindow != null)
                _owners.Push(rootWindow);
        }

        public bool? Show(IDialogViewModel viewModel)
        {
            var window = new DialogWindow();
            window.DataContext = viewModel;
            viewModel.OnShow();
            window.Owner = _owners.FirstOrDefault();
            _owners.Push(window);
            try
            {
                return window.ShowDialog();
            }
            finally
            {
                _owners.Pop();
            }
        }
    }
}
