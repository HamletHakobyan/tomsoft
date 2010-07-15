using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Service
{
    public interface IDialogService
    {
        bool? Show(IDialogViewModel viewModel);
    }
}
