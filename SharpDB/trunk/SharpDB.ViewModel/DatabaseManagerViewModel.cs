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
                    SelectedDatabase = _selectedItem as DatabaseViewModel;
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
                Databases.Add(new DatabaseViewModel(connectionVM.DatabaseConnection));
            }
        }

        private void EditConnection()
        {
            if (SelectedDatabase == null)
                return;

            var service = GetService<IDialogService>();
            var connectionVM = new ConnectionDialogViewModel(SelectedDatabase.DatabaseConnection);
            if (service.Show(connectionVM) == true)
            {
                var config = GetService<Config>();
                config.Save();
                SelectedDatabase.Refresh();
            }
        }

        private void DeleteConnection()
        {
            if (SelectedDatabase == null)
                return;

            var text = ResourceManager.GetString("confirm_delete_connection");
            var title = ResourceManager.GetString("confirm_delete_connection_title");
            var mboxService = GetService<IMessageBoxService>();
            if (mboxService.Show(text, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (SelectedDatabase.IsConnected)
                    SelectedDatabase.Disconnect();
                var config = GetService<Config>();
                config.Connections.Remove(SelectedDatabase.DatabaseConnection);
                config.Save();
                Databases.Remove(SelectedDatabase);
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

    }
}
