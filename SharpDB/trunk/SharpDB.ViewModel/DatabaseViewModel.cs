﻿using System;
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

namespace SharpDB.ViewModel
{
    public class DatabaseViewModel : ViewModelBase
    {
        #region Private data

        private DatabaseConnection _databaseConnection;
        private DbConnection _connection;

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

        #endregion

        #region Commands

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
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _connection.Close();
                OnPropertyChanged(() => IsConnected);
                OnPropertyChanged(() => IsBusy);

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

        public void Refresh()
        {
            OnPropertyChanged();
        }

        #endregion
    }
}
