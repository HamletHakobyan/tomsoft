using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Dialogs;
using SharpDB.Model;

namespace SharpDB.ViewModel
{
    public class ConnectionDialogViewModel : ViewModelBase, IDialogViewModel
    {
        private DialogButton[] _buttons;

        private DatabaseConnection _connection;

        public DatabaseConnection DatabaseConnection
        {
            get { return _connection; }
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

        public ConnectionDialogViewModel(DatabaseConnection connection)
        {
            // LOCALIZE
            _connection = connection ?? new DatabaseConnection { Name = "New database connection" };
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
        }

        public ConnectionDialogViewModel()
            : this(null)
        {
        }

        public string DialogTitle
        {
            get { return ResourceManager.GetString("connection_dialog_title"); }
        }

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
    }
}
