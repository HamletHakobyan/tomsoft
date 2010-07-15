using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Service
{
    public interface IDialogViewModel
    {
        string DialogTitle { get; }
        IEnumerable<DialogButton> Buttons { get; }
        bool Resizable { get; }

        void OnShow();
        void OnClose(bool? result);
        event CloseRequestedEventHandler CloseRequested;
    }
}
