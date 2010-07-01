using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using SharpDB.Util.Dialogs;

namespace SharpDB.ViewModel
{
    public class WorksheetViewModel : ViewModelBase
    {
        private static int _worksheetNum = 0;

        public WorksheetViewModel()
        {
            _title = string.Format("Untitled{0}", ++_worksheetNum);
        }

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private bool _isModified;
        public bool IsModified
        {
            get { return _isModified; }
            set
            {
                if (value != _isModified)
                {
                    _isModified = value;
                    OnPropertyChanged("IsModified");
                }
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand(
                            Save,
                            () => IsModified);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Public methods

        public void Save()
        {
            GetService<IMessageBoxService>().Show("Not implemented");
        }

        #endregion
    }
}
