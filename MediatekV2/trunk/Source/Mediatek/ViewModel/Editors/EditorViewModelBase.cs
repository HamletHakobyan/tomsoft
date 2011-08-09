using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mediatek.Service;

namespace Mediatek.ViewModel.Editors
{
    public class EditorViewModelBase : DialogViewModelBase
    {
        private readonly DialogButton _okButton;
        private readonly DialogButton _cancelButton;

        public EditorViewModelBase()
        {
            _okButton = new DialogButton
                {
                    Text = Properties.Resources.general_ok,
                    IsDefault = true,
                    DialogResult = true
                };
            _cancelButton = new DialogButton
            {
                Text = Properties.Resources.general_cancel,
                IsCancel = true,
                DialogResult = false
            };

            Buttons.Add(_okButton);
            Buttons.Add(_cancelButton);
        }

        public DialogButton OKButton
        {
            get { return _okButton; }
        }

        public DialogButton CancelButton
        {
            get { return _cancelButton; }
        }

        protected override void OnShow()
        {
            base.OnShow();
            Load();
        }

        protected override void OnClose(bool? dialogResult)
        {
            base.OnClose(dialogResult);
            if (dialogResult == true)
            {
                Save();
            }
            else
            {
                Load();
            }
        }

        protected virtual void Load()
        {
        }

        protected virtual void Save()
        {
        }
    }
}
