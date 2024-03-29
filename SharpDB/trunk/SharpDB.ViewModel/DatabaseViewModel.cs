﻿using System;
using System.Linq;
using Developpez.Dotnet.Windows.Service;
using SharpDB.Model;
using System.Data.Common;
using System.Data;
using Developpez.Dotnet;
using Developpez.Dotnet.Data;
using System.Collections.ObjectModel;
using SharpDB.Util.Service;
using SharpDB.Model.Data;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using SharpDB.ViewModel.DbModel;
using System.Windows;

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
                    _model = DbProviderHelper.GetDbModel(_databaseConnection.ProviderName);
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
                try
                {
                    WrapExceptionForDbAction(() =>
                    {

                        var factory = DbProviderFactories.GetFactory(_databaseConnection.ProviderName);
                        _connection = factory.CreateConnection();
                        _connection.ConnectionString = _databaseConnection.ConnectionString;
                        _connection.Open();
                    });
                }
                catch (DbException ex)
                {
                    var service = GetService<IMessageBoxService>();
                    service.Show(ex.Message, GetResource<string>("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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

                var config = GetService<Config>();
                config.RecentConnections.Add(ConnectionName);
                while (config.RecentConnections.Count > config.MaxRecentItems)
                    config.RecentConnections.RemoveAt(config.RecentConnections.Count - 1);
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
            DbDataReader reader = null;
            WrapExceptionForDbAction(() =>
            {
                using (var command = _connection.CreateCommand(commandText))
                {
                    reader = command.ExecuteReader();
                }
            });
            return reader;
        }

        public int ExecuteNonQuery(string commandText)
        {
            CheckConnected();
            int result = 0;
            WrapExceptionForDbAction(() =>
                {
                    using (var command = _connection.CreateCommand(commandText))
                    {
                        try
                        {
                            result = command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            if (ex is DbException)
                                throw;
                            else
                                throw new DbExceptionWrapper(ex);
                        }
                    }
                });
            return result;
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

        private void WrapExceptionForDbAction(Action action)
        {
            try
            {
                action();
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
}
