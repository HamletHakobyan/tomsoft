using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VBulletinBox.Commands;
using System.Collections.ObjectModel;
using VBulletinBox.Models;
using System.Xml.Serialization;
using System.IO;

namespace VBulletinBox.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor

        public MainViewModel()
        {
            var accounts = GetAccounts();
            _accounts = new ObservableCollection<AccountViewModel>(accounts.Select(a => new AccountViewModel(a)));
        }

        #endregion

        #region Private members

        private IEnumerable<VBulletinAccount> GetAccounts()
        {
            if (File.Exists(AccountsFilePath))
            {
                var xs = new XmlSerializer(typeof(List<VBulletinAccount>));
                using (StreamReader rd = new StreamReader(AccountsFilePath))
                {
                    var accounts = (List<VBulletinAccount>)xs.Deserialize(rd);
                    return accounts;
                }
            }
            return new List<VBulletinAccount>();
        }

        #endregion

        #region Public methods

        public void SaveAccounts()
        {
            var accounts = _accounts.Select(a => a.Model).ToList();
            var xs = new XmlSerializer(typeof(List<VBulletinAccount>));
            using (StreamWriter wr = new StreamWriter(AccountsFilePath))
            {
                xs.Serialize(wr, accounts);
            }
        }

        #endregion

        #region Public properties

        private string _dataDirectoryPath;
        public string DataDirectoryPath
        {
            get
            {
                if (_dataDirectoryPath == null)
                {
                    _dataDirectoryPath =
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "VBulletinBox");
                    if (!Directory.Exists(_dataDirectoryPath))
                        Directory.CreateDirectory(_dataDirectoryPath);
                }
                return _dataDirectoryPath;
            }
        }

        private string _accountsFilePath;
        public string AccountsFilePath
        {
            get
            {
                if (_accountsFilePath == null)
                {
                    _accountsFilePath = Path.Combine(DataDirectoryPath, "accounts.xml"); ;
                }
                return _accountsFilePath;
            }
        }
    

        private ObservableCollection<AccountViewModel> _accounts;
        public ObservableCollection<AccountViewModel> Accounts
        {
            get { return _accounts; }
            set
            {
                if (value != _accounts)
                {
                    _accounts = value;
                    OnPropertyChanged(() => Accounts);
                }
            }
        }

        private RepositoryViewModel _currentRepository;
        public RepositoryViewModel CurrentRepository
        {
            get { return _currentRepository; }
            set
            {
                _currentRepository = value;
                OnPropertyChanged(() => CurrentRepository);
            }
        }

        private FolderViewModel _currentFolder;
        public FolderViewModel CurrentFolder
        {
            get { return _currentFolder; }
            set
            {
                _currentFolder = value;
                OnPropertyChanged(() => CurrentFolder);
                if (_currentFolder != null)
                    CurrentMessage = _currentFolder.CurrentMessage;
                else
                    CurrentMessage = null;
            }
        }

        private MessageViewModel _currentMessage;
        public MessageViewModel CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                _currentMessage = value;
                OnPropertyChanged(() => CurrentMessage);
            }
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(() => SelectedItem);
                if (_selectedItem is FolderViewModel)
                {
                    CurrentFolder = _selectedItem as FolderViewModel;
                }
                else
                {
                    if (_selectedItem is RepositoryViewModel)
                    {
                        CurrentFolder = null;
                        CurrentRepository = _selectedItem as RepositoryViewModel;
                    }
                }
            }
        }


        #endregion

        #region Commands
        
        private DelegateCommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new DelegateCommand(Exit);
                }
                return _exitCommand;
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private DelegateCommand _getMessagesCommand;
        public ICommand GetMessagesCommand
        {
            get
            {
                if (_getMessagesCommand == null)
                {
                    _getMessagesCommand = new DelegateCommand(GetMessages);
                }
                return _getMessagesCommand;
            }
        }

        private void GetMessages()
        {
            foreach (AccountViewModel account in _accounts)
            {
                account.GetMessages();
            }
        }

        private DelegateCommand _newAccountCommand;
        public ICommand NewAccountCommand
        {
            get
            {
                if (_newAccountCommand == null)
                {
                    _newAccountCommand = new DelegateCommand(NewAccount);
                }
                return _newAccountCommand;
            }
        }

        private void NewAccount()
        {
            VBulletinAccount account = new VBulletinAccount();
            account.DisplayName = "New account";
            AccountViewModel vm = new AccountViewModel(account);
            if (vm.ShowProperties())
            {
                account.RepositoryFile = Path.Combine(DataDirectoryPath, string.Format(@"Repositories\{0}.xml", account.DisplayName));
                this.Accounts.Add(vm);
            }
        }

        #endregion

    }
}
