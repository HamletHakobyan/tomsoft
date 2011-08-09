using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mediatek.Service
{
    public abstract class DialogViewModelBase : WindowViewModelBase, IDialogViewModel
    {
        private readonly ObservableCollection<DialogButton> _buttons;

        protected DialogViewModelBase()
        {
            _buttons = new ObservableCollection<DialogButton>();
        }

        public string DialogTitle { get; set; }

        public IList<DialogButton> Buttons
        {
            get { return _buttons; }
        }

        IEnumerable<DialogButton> IDialogViewModel.Buttons
        {
            get { return _buttons; }
        }
    }
}