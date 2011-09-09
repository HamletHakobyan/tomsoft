using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Developpez.Dotnet.Windows.Service;
using SharpDB.Model.Data;
using SharpDB.Util;
using SharpDB.Model;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using SharpDB.Util.Service;
using System.Windows;

namespace SharpDB.ViewModel
{
    public class DatabaseManagerViewModel : ViewModelBase
    {
        public DatabaseManagerViewModel()
        {
            if (!IsInDesignMode)
            {
                var config = GetService<Config>();
                _databases = new ObservableCollection<DatabaseViewModel>(
                    config.Connections.Select(c => new DatabaseViewModel(c))
                );
            }
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

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    var db = value as DatabaseViewModel;
                    if (db == null)
                    {
                        var dbChild = value as IDatabaseChildItem;
                        if (dbChild != null)
                            db = dbChild.Database;
                    }
                    SelectedDatabase = db;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }


        private DatabaseViewModel _selectedDatabase;
        public DatabaseViewModel SelectedDatabase
        {
            get { return _selectedDatabase; }
            set
            {
                if (value != _selectedDatabase)
                {
                    _selectedDatabase = value;
                    OnPropertyChanged("SelectedDatabase");
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

        private DelegateCommand _editConnectionCommand;
        public ICommand EditConnectionCommand
        {
            get
            {
                if (_editConnectionCommand == null)
                {
                    _editConnectionCommand =
                        new DelegateCommand(
                            EditConnection,
                            () => SelectedDatabase != null);
                }
                return _editConnectionCommand;
            }
        }

        private DelegateCommand _deleteConnectionCommand;
        public ICommand DeleteConnectionCommand
        {
            get
            {
                if (_deleteConnectionCommand == null)
                {
                    _deleteConnectionCommand =
                        new DelegateCommand(
                            DeleteConnection,
                            () => SelectedDatabase != null);
                }
                return _deleteConnectionCommand;
            }
        }

        private DelegateCommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                {
                    _connectCommand =
                        new DelegateCommand(
                            Connect,
                            () => SelectedDatabase != null && !SelectedDatabase.IsConnected);
                }
                return _connectCommand;
            }
        }

        private DelegateCommand _disconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                if (_disconnectCommand == null)
                {
                    _disconnectCommand =
                        new DelegateCommand(
                            Disconnect,
                            () => SelectedDatabase != null && SelectedDatabase.IsConnected);
                }
                return _disconnectCommand;
            }
        }

        private DelegateCommand _refreshModelCommand;
        public ICommand RefreshModelCommand
        {
            get
            {
                if (_refreshModelCommand == null)
                {
                    _refreshModelCommand =
                        new DelegateCommand(
                            RefreshModel,
                            () => SelectedDatabase != null && SelectedDatabase.IsConnected);
                }
                return _refreshModelCommand;
            }
        }


        #endregion

        private void NewConnection()
        {
            var service = GetService<IDialogService>();
            var connectionVM = new ConnectionDialogViewModel();
            if (service.Show(connectionVM) == true)
            {
                var config = GetService<Config>();
                config.Connections.Add(connectionVM.DatabaseConnection);
                config.Save();
                var database = new DatabaseViewModel(connectionVM.DatabaseConnection);
                Databases.Add(database);

                Mediator.Instance.Post(this, new DatabaseAddedMessage
                {
                    Database = database
                });
            }
        }

        private void EditConnection()
        {
            var database = SelectedDatabase;
            if (database == null)
                return;

            var service = GetService<IDialogService>();
            var connectionVM = new ConnectionDialogViewModel(database.DatabaseConnection);
            if (service.Show(connectionVM) == true)
            {
                var config = GetService<Config>();
                config.Save();
                database.Refresh();

                Mediator.Instance.Post(this, new DatabaseChangedMessage
                {
                    Database = database
                });
            }
        }

        private void DeleteConnection()
        {
            var database = SelectedDatabase;
            if (database == null)
                return;

            var text = GetResource<string>("confirm_delete_connection");
            var title = GetResource<string>("confirm_delete_connection_title");
            var mboxService = GetService<IMessageBoxService>();
            if (mboxService.Show(text, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (database.IsConnected)
                    database.Disconnect();
                var config = GetService<Config>();
                config.Connections.Remove(database.DatabaseConnection);
                config.Save();
                Databases.Remove(database);

                Mediator.Instance.Post(this, new DatabaseRemovedMessage
                {
                    Database = database
                });
            }
        }

        private void Connect()
        {
            if (SelectedDatabase == null || SelectedDatabase.IsConnected)
                return;

            SelectedDatabase.Connect();
        }

        private void Disconnect()
        {
            if (SelectedDatabase == null || !SelectedDatabase.IsConnected)
                return;

            SelectedDatabase.Disconnect();
        }

        private void RefreshModel()
        {
            if (SelectedDatabase == null || !SelectedDatabase.IsConnected)
                return;
            SelectedDatabase.RefreshModel();
        }

        public bool ConnectByName(string databaseName)
        {
            var database = _databases.FirstOrDefault(db => db.ConnectionName == databaseName);
            if (database != null)
            {
                if (!database.IsConnected)
                {
                    database.Connect();
                }
                return true;
            }
            return false;
        }

        public void OnFileDrop(string[] files)
        {
            foreach (var file in files)
            {
                OnFileDrop(file);
            }
        }

        public void OnFileDrop(string file)
        {
            var fileHandlers =
                (from row in DbProviderFactories.GetFactoryClasses().AsEnumerable()
                 let handler = DbProviderHelper.GetFileHandler((string)row["InvariantName"])
                 where handler != null && handler.CanUseFile(file)
                 select handler).ToArray();

            
            if (!fileHandlers.Any())
            {
                GetService<IMessageBoxService>().Show(GetResource<string>("no_handler_for_file"), GetResource<string>("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var fileHandler = fileHandlers.First();
            var connectionVM = new ConnectionDialogViewModel();
            connectionVM.DatabaseConnection.ProviderName = fileHandler.ProviderName;
            connectionVM.DatabaseConnection.ConnectionString = fileHandler.MakeConnectionString(file);
            var service = GetService<IDialogService>();
            if (service.Show(connectionVM) == true)
            {
                var config = GetService<Config>();
                config.Connections.Add(connectionVM.DatabaseConnection);
                config.Save();
                var database = new DatabaseViewModel(connectionVM.DatabaseConnection);
                Databases.Add(database);
                Mediator.Instance.Post(this, new DatabaseAddedMessage
                {
                    Database = database
                });
            }
        }
    }
}
