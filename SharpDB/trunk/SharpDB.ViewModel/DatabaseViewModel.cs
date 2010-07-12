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
using SharpDB.Util.Dialogs;

namespace SharpDB.ViewModel
{
    public class DatabaseViewModel : ViewModelBase, IDialogViewModel
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

        private ObservableCollection<IEnumerable> _metadataCollections;
        public ObservableCollection<IEnumerable> MetadataCollections
        {
            get { return _metadataCollections; }
            set
            {
                if (value != _metadataCollections)
                {
                    _metadataCollections = value;
                    OnPropertyChanged("MetadataCollections");
                }
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

                OnPropertyChanged(() => IsConnected);
                OnPropertyChanged(() => IsBusy);
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _connection.Close();
                OnPropertyChanged(() => IsConnected);
                OnPropertyChanged(() => IsBusy);
            }
        }

        public DbDataReader ExecuteReader(string commandText)
        {
            CheckConnected();
            using (var command = _connection.CreateCommand(commandText))
            {
                return command.ExecuteReader();
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            CheckConnected();
            using (var command = _connection.CreateCommand(commandText))
            {
                return command.ExecuteNonQuery();
            }
        }

        private void CheckConnected()
        {
            if (!IsConnected)
                // LOCALIZE
                throw new InvalidOperationException("Not connected");
        }

        #endregion

        #region IDialogViewModel implementation

        string IDialogViewModel.DialogTitle
        {
            get { return ResourceManager.GetString("connection_dialog_title"); }
        }

        private DialogButton[] _buttons;
        IEnumerable<DialogButton> IDialogViewModel.Buttons
        {
            get { return _buttons; }
        }

        bool IDialogViewModel.Resizable
        {
            get { return true; }
        }

        void IDialogViewModel.OnShow()
        {
            _saveConnectionName = _databaseConnection.Name;
            _saveProviderName = _databaseConnection.ProviderName;
            _saveConnectionString = _databaseConnection.ConnectionString;
        }

        void IDialogViewModel.OnClose(bool? result)
        {
            if (result != true)
            {
                _databaseConnection.Name = _saveConnectionName;
                _databaseConnection.ProviderName = _saveProviderName;
                _databaseConnection.ConnectionString = _saveConnectionString;
            }
        }

        event CloseRequestedEventHandler IDialogViewModel.CloseRequested
        {
            add { }
            remove { }
        }

        private string _saveConnectionName;
        private string _saveProviderName;
        private string _saveConnectionString;

        #endregion
    }
}
