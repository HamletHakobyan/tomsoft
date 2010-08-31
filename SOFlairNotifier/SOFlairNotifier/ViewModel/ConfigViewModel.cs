using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;

namespace SOFlairNotifier.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        public ConfigViewModel()
        {
            Reload();
        }

        #region Properties

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        private TimeSpan _checkFrequency;
        public TimeSpan CheckFrequency
        {
            get { return _checkFrequency; }
            set
            {
                _checkFrequency = value;
                OnPropertyChanged("CheckFrequency");
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
                    _saveCommand = new DelegateCommand(Save);
                }
                return _saveCommand;
            }
        }

        private DelegateCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new DelegateCommand(Reload);
                }
                return _cancelCommand;
            }
        }

        private DelegateCommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new DelegateCommand(Reset);
                }
                return _resetCommand;
            }
        }

        #endregion

        #region Public methods

        public void Reload()
        {
            Properties.Settings.Default.Reload();
            UserId = Properties.Settings.Default.UserId;
            CheckFrequency = Properties.Settings.Default.CheckFrequency;
        }

        public void Save()
        {
            Properties.Settings.Default.UserId = UserId;
            Properties.Settings.Default.CheckFrequency = CheckFrequency;
            Properties.Settings.Default.Save();
        }

        public void Reset()
        {
            Properties.Settings.Default.Reset();
            Reload();
        }

        #endregion
    }
}
