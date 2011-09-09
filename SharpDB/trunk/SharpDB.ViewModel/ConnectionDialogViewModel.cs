using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using SharpDB.Model.Data;
using SharpDB.Util.Service;
using SharpDB.Model;
using System.Data;
using System.Data.Common;

namespace SharpDB.ViewModel
{
    public class ConnectionDialogViewModel : ViewModelBase, IDialogViewModel
    {
        private readonly DialogButton[] _buttons;
        private readonly DatabaseConnection _connection;
        private IConnectionStringEditor _connectionStringEditor;
        private bool _isDefaultName;

        #region Constructors

        public ConnectionDialogViewModel(DatabaseConnection connection)
        {
            _connection = connection;
            if (_connection == null)
            {
                _connection = new DatabaseConnection { Name = GetResource<string>("connection_default_name") };
                _isDefaultName = true;
            }

            this.DialogTitle = connection == null
                                ? GetResource<string>("new_database_connection")
                                : GetResource<string>("edit_database_connection");

            _buttons = new[]
            {
                new DialogButton
                {
                    Text = GetResource<string>("dialog_ok"),
                    DialogResult = true,
                    IsDefault = true
                },
                new DialogButton
                {
                    Text = GetResource<string>("dialog_cancel"),
                    DialogResult = false,
                    IsCancel = true
                }
            };

            _dbProviders = DbProviderFactories.GetFactoryClasses();
        }

        public ConnectionDialogViewModel()
            : this(null)
        {
        }

        #endregion

        #region Properties

        public DatabaseConnection DatabaseConnection
        {
            get { return _connection; }
        }

        private DataTable _dbProviders;
        public DataTable DbProviders
        {
            get { return _dbProviders; }
            set
            {
                if (value != _dbProviders)
                {
                    _dbProviders = value;
                    OnPropertyChanged("DbProviders");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _isDefaultName = false;
                OnPropertyChanged("Name");
            }
        }

        private string _providerName;
        public string ProviderName
        {
            get { return _providerName; }
            set
            {
                _providerName = value;
                _connectionStringEditor = string.IsNullOrEmpty(_providerName)
                        ? null
                        : DbProviderHelper.GetConnectionStringEditor(_providerName);
                OnPropertyChanged("ProviderName");
                OnPropertyChanged("CanEditConnectionString");
                
            }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                OnPropertyChanged("ConnectionString");
            }
        }

        public bool CanEditConnectionString
        {
            get { return _connectionStringEditor != null; }
        }

        #endregion

        #region IDialogViewModel implementation

        public string DialogTitle { get; private set; }

        public IEnumerable<DialogButton> Buttons
        {
            get { return _buttons; }
        }

        public bool Resizable
        {
            get { return true; }
        }

        public void OnShow()
        {
            if (_connection != null)
            {
                _name = _connection.Name;
                _providerName = _connection.ProviderName;
                _connectionString = _connection.ConnectionString;
                _connectionStringEditor = string.IsNullOrEmpty(_providerName)
                    ? null
                    : DbProviderHelper.GetConnectionStringEditor(_providerName);
            }
            else
            {
                _name = GetResource<string>("connection_default_name");
                _providerName = null;
                _connectionString = null;
                _isDefaultName = true;
            }
        }

        public void OnClose(bool? result)
        {
            if (result == true)
            {
                _connection.Name = _name;
                _connection.ProviderName = _providerName;
                _connection.ConnectionString = _connectionString;
            }
        }

        public event CloseRequestedEventHandler CloseRequested
        {
            add { }
            remove { }
        }

        #endregion

        #region Commands

        private DelegateCommand _editConnectionStringCommand;
        public ICommand EditConnectionStringCommand
        {
            get
            {
                if (_editConnectionStringCommand == null)
                {
                    _editConnectionStringCommand = new DelegateCommand(EditConnectionString, () => CanEditConnectionString);
                }
                return _editConnectionStringCommand;
            }
        }

        private void EditConnectionString()
        {
            if (_connectionStringEditor != null)
            {
                _connectionStringEditor.ConnectionString = ConnectionString;
                if (_connectionStringEditor.ShowDialog() == true)
                {
                    ConnectionString = _connectionStringEditor.ConnectionString;
                    if (string.IsNullOrEmpty(Name) || _isDefaultName)
                    {
                        Name = _connectionStringEditor.GetDefaultName();
                        _isDefaultName = true;
                    }
                }
            }
        }

        #endregion
    }
}
