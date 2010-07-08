using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SharpDB.Util;
using SharpDB.Model;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using SharpDB.Util.Dialogs;

namespace SharpDB.ViewModel
{
    public class DatabaseManagerViewModel : ViewModelBase
    {
        public DatabaseManagerViewModel()
        {
            var config = GetService<Config>();
            _databases = new ObservableCollection<DatabaseViewModel>(
                config.Connections.Select(c => new DatabaseViewModel(c))
            );
        }

        #region Properties

        private ObservableCollection<DatabaseViewModel> _databases;
        public ObservableCollection<DatabaseViewModel> Databases
        {
            get { return _databases; }
            set
            {
                if (value != _databases)
                {
                    _databases = value;
                    OnPropertyChanged("Databases");
                }
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _newConnectionCommand;
        public ICommand NewConnectionCommand
        {
            get
            {
                if (_newConnectionCommand == null)
                {
                    _newConnectionCommand = new DelegateCommand(NewConnection);
                }
                return _newConnectionCommand;
            }
        }

        private void NewConnection()
        {
            var service = GetService<IDataConnectionDialogService>();
            var databaseConnection = new DatabaseConnection { Name = "New database connection" };
            if (service.Show(databaseConnection) == true)
            {
                Databases.Add(new DatabaseViewModel(databaseConnection));
            }
        }


        #endregion
    }
}
