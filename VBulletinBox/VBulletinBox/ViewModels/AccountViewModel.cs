using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VBulletinBox.Models;
using Developpez.Dotnet;
using System.IO;
using VBulletinBox.Util;
using VBulletinBox.Views;
using VBulletinBox.Commands;
using System.Windows.Input;
using Microsoft.Win32;

namespace VBulletinBox.ViewModels
{
    public class AccountViewModel : ViewModelBase<VBulletinAccount>
    {
        private readonly VBulletinAccount _account;

        public AccountViewModel(VBulletinAccount account)
        {
            this._account = account;
            Reset();
        }

        #region Public properties

        public override VBulletinAccount Model
        {
            get { return _account; }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string _siteUrl;
        public string SiteUrl
        {
            get { return _siteUrl; }
            set { _siteUrl = value; }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password ?? "********"; }
            set { _password = value; }
        }

        private RepositoryViewModel _repository;
        public RepositoryViewModel Repository
        {
            get
            {
                if (_repository == null)
                {
                    MessageRepository rep;
                    if (File.Exists(_account.RepositoryFile))
                    {
                        rep = MessageRepository.FromFile(_account.RepositoryFile, _account);
                    }
                    else
                    {
                        rep = new MessageRepository();
                    }
                    _repository = new RepositoryViewModel(rep);
                }
                return _repository;
            }
            set { _repository = value; }
        }

        private string _repositoryFile;
        public string RepositoryFile
        {
            get { return _repositoryFile; }
            set
            {
                _repositoryFile = value;
                OnPropertyChanged("RepositoryFile");
            }
        }


        #endregion

        #region Public methods

        public void Save()
        {
            _account.DisplayName = _displayName;
            _account.SiteUrl = _siteUrl;
            _account.UserName = _userName;
            if (_password != null)
                _account.PasswordMD5 = _password.GetMD5Digest();
            _account.RepositoryFile = _repositoryFile;
            _repository = null;
            OnPropertyChanged(string.Empty);
            App.Current.ViewModel.SaveAccounts();
        }

        public void Reset()
        {
            this.DisplayName = _account.DisplayName;
            this.SiteUrl = _account.SiteUrl;
            this.UserName = _account.UserName;
            this.RepositoryFile = _account.RepositoryFile;
            this.Password = null;
        }

        public void GetMessages()
        {
            VBulletinClient client = new VBulletinClient(_account);
            MessageRepository rep = client.GetMessages();
            _repository.Merge(rep);
            App.Current.ViewModel.CurrentFolder = null;
            _repository.Save(_account.RepositoryFile);
        }

        public bool ShowProperties()
        {
            EditorDialog dlg = new EditorDialog(this, typeof(AccountView));
            if (dlg.ShowDialog() == true)
            {
                this.Save();
                return true;
            }
            this.Reset();
            return false;
        }

        #endregion

        #region Commands

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

        private DelegateCommand _showPropertiesCommand;
        public ICommand ShowPropertiesCommand
        {
            get
            {
                if (_showPropertiesCommand == null)
                {
                    _showPropertiesCommand = new DelegateCommand(() => ShowProperties());
                }
                return _showPropertiesCommand;
            }
        }

        private DelegateCommand _importFileCommand;
        public ICommand ImportFileCommand
        {
            get
            {
                if (_importFileCommand == null)
                {
                    _importFileCommand = new DelegateCommand(ImportFile);
                }
                return _importFileCommand;
            }
        }

        private DelegateCommand _browseRepositoryCommand;
        public ICommand BrowseRepositoryCommand
        {
            get
            {
                if (_browseRepositoryCommand == null)
                {
                    _browseRepositoryCommand = new DelegateCommand(BrowseRepository);
                }
                return _browseRepositoryCommand;
            }
        }

        private void BrowseRepository()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML Files|*.xml";
            dlg.FileName = RepositoryFile;
            if (!RepositoryFile.IsNullOrEmpty())
                dlg.InitialDirectory = Path.GetDirectoryName(RepositoryFile);
            if (dlg.ShowDialog() == true)
            {
                RepositoryFile = dlg.FileName;
            }
        }


        private void ImportFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files|*.xml";
            ofd.FileName = "";
            if (ofd.ShowDialog() == true)
            {
                var rep = MessageRepository.FromFile(ofd.FileName, null);
                _repository.Merge(rep);
                App.Current.ViewModel.CurrentFolder = null;
                _repository.Save(_account.RepositoryFile);
            }
        }

        #endregion

    }
}
