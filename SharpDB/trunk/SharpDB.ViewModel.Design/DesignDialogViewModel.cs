using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Dialogs;

namespace SharpDB.ViewModel.Design
{
    public class DesignDialogViewModel : IDialogViewModel
    {
        private DialogButton[] _buttons;

        public DesignDialogViewModel()
        {
            _buttons = new[]
            {
                new DialogButton { Text = "OK", IsDefault = true },
                new DialogButton { Text = "Cancel", IsCancel = false }
            };
        }

        public string DialogTitle
        {
            get { return "Design-time dialog"; }
        }

        public IEnumerable<DialogButton> Buttons
        {
            get { return _buttons; }
        }

        public bool Resizable
        {
            get { return true; }
        }

        public void OnShow()
        {
        }

        public void OnClose(bool? result)
        {
        }

        public event CloseRequestedEventHandler CloseRequested;
    }
}
