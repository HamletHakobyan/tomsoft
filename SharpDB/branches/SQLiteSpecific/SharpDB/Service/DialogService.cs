using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Service;
using System.Windows;

namespace SharpDB.Service
{
    class DialogService : DialogServiceBase, IDialogService
    {
        public bool? Show(IDialogViewModel viewModel)
        {
            var window = new DialogWindow();
            window.Owner = GetTopWindow();
            using(PushWindow(window))
            {
                return window.Show(viewModel);
            }
        }
    }
}
