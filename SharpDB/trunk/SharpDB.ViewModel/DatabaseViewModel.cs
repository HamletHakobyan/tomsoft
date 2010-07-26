using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model;
using System.Data.Common;
using System.Data;
using Developpez.Dotnet;
using Developpez.Dotnet.Data;
using System.Collections;
using System.Collections.ObjectModel;
using SharpDB.Util.Service;
using SharpDB.Model.Data;
using Developpez.Dotnet.Windows.Util;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using SharpDB.ViewModel.DbModel;

namespace SharpDB.ViewModel
{
    public class DatabaseViewModel : ViewModelBase
    {
        #region Private data

        private DatabaseConnection _databaseConnection;
        private DbConnection _connection;
        private IDbModel _model;

        #endregion

        #region Constructor

        public DatabaseViewModel(DatabaseConnection databaseConnection)
        {
            databaseConnection.CheckArgumentNull("databaseConnection");
            _databaseConnection = databaseConnection;
        }

        #endregion

        #region Properties

        public DatabaseConnection DatabaseConnection
        {
            get { return _databaseConnection; }
        }

        public string ConnectionName
        {
            get
            {
                if (_databaseConnection == null)
                    return null;
                return _databaseConnection.Name;
            }
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.State.HasFlag(ConnectionState.Open);
            }
        }

        public bool IsBusy
        {
            get
            {
                return _connection != null &&
                    (_connection.State.HasFlag(ConnectionState.Connecting) ||
                    _connection.State.HasFlag(ConnectionState.Executing) ||
                    _connection.State.HasFlag(ConnectionState.Fetching));
            }
        }

        private ObservableCollection<DbModelItemGroupViewModel> _modelGroups;
        public ObservableCollection<DbModelItemGroupViewModel> ModelGroups
        {
            get
            {
                if (_modelGroups == null && IsConnected)
                {
                    _model = DbModelHelper.GetModel(_databaseConnection.ProviderName);
                    if (_model != null)
                    {
                        _model.InitModel(_connection);
                        var groups = _model.ItemGroups.Select(g => new DbModelItemGroupViewModel(this, g));
                        _modelGroups = new ObservableCollection<DbModelItemGroupViewModel>(groups);
                    }
                }
                return _modelGroups;
            }
        }


        #endregion

        #region Commands

        private DelegateCommand<MouseButtonEventArgs> _databaseDoubleClickCommand;
        public ICommand DatabaseDoubleClickCommand
        {
            get
            {
                if (_databaseDoubleClickCommand == null)
                {
                    _databaseDoubleClickCommand = new DelegateCommand<MouseButtonEventArgs>(DatabaseDoubleClick);
                }
                return _databaseDoubleClickCommand;
            }
        }

        #endregion

        #region Public methods

        public void Connect()
        {
            if (!IsConnected)
            {
                var factory = DbProviderFactories.GetFactory(_databaseConnection.ProviderName);
                _connection = factory.CreateConnection();
                _connection.ConnectionString = _databaseConnection.ConnectionString;
                _connection.Open();
                //var schema = new DbSchema(_connection);

                OnPropertyChanged(() => IsConnected);
                OnPropertyChanged(() => IsBusy);
                OnPropertyChanged(() => ModelGroups);

                Mediator.Instance.Post(this, new ConnectionStateChangedMessage
                {
                    Database = this,
                    IsConnected = true
                });

                string arguments = string.Format("/connect \"{0}\"", ConnectionName);
                GetService<IJumpListService>().AddTask(
                    ConnectionName,
                    GetResource<string>("jumplist_recent_databases"),
                    args: arguments);

                GetService<Config>().RecentConnections.Add(ConnectionName);
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _connection.Close();
                _model = null;
                _modelGroups = null;
                OnPropertyChanged(() => IsConnected);
                OnPropertyChanged(() => IsBusy);
                OnPropertyChanged(() => ModelGroups);

                Mediator.Instance.Post(this, new ConnectionStateChangedMessage
                {
                    Database = this,
                    IsConnected = false
                });
            }
        }

        public DbDataReader ExecuteReader(string commandText)
        {
            CheckConnected();
            using (var command = _connection.CreateCommand(commandText))
            {
                try
                {
                    return command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    if (ex is DbException)
                        throw;
                    else
                        throw new DbExceptionWrapper(ex);
                }

            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            CheckConnected();
            using (var command = _connection.CreateCommand(commandText))
            {
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (ex is DbException)
                        throw;
                    else
                        throw new DbExceptionWrapper(ex);
                }
            }
        }

        public void RefreshModel()
        {
            _model = null;
            _modelGroups = null;
            OnPropertyChanged(() => ModelGroups);
        }

        public void Refresh()
        {
            OnPropertyChanged();
        }

        #endregion

        private bool CheckConnected()
        {
            if (!IsConnected)
            {
                var service = GetService<IMessageBoxService>();
                string message = GetResource<string>("database_not_connected");
                service.Show(message);
                return false;
            }
            return true;
        }

        private void DatabaseDoubleClick(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Connect();
            }
        }
    }
}
