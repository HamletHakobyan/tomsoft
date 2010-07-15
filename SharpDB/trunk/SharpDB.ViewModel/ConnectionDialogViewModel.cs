using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Service;
using SharpDB.Model;
using System.Data;
using System.Data.Common;

namespace SharpDB.ViewModel
{
    public class ConnectionDialogViewModel : ViewModelBase, IDialogViewModel
    {
        private DialogButton[] _buttons;
        private DatabaseConnection _connection;

        #region Constructors

        public ConnectionDialogViewModel(DatabaseConnection connection)
        {
            _connection = connection ?? new DatabaseConnection { Name = ResourceManager.GetString("connection_default_name") };

            this.DialogTitle = connection == null
                                ? ResourceManager.GetString("new_database_connection")
                                : ResourceManager.GetString("edit_database_connection");

            _buttons = new[]
            {
                new DialogButton
                {
                    Text = ResourceManager.GetString("dialog_ok"),
                    DialogResult = true,
                    IsDefault = true
                },
                new DialogButton
                {
                    Text = ResourceManager.GetString("dialog_cancel"),
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
                OnPropertyChanged("ProviderName");
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
                Name = _connection.Name;
                ProviderName = _connection.ProviderName;
                ConnectionString = _connection.ConnectionString;
            }
            else
            {
                Name = "New database connection";
                ProviderName = null;
                ConnectionString = null;
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
    }
}
