using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Service.Implementation
{
    public class DialogService : IDialogService
    {
        public bool? Show(IDialogViewModel viewModel)
        {
            var window = new DialogWindow();
            window.DataContext = viewModel;
            return window.ShowDialog();
        }
    }
}
