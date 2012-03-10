using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VBulletinBox.Models;
using System.Collections.ObjectModel;
using VBulletinBox.Commands;
using VBulletinBox.Util;
using System.Windows.Input;

namespace VBulletinBox.ViewModels
{
    public class RepositoryViewModel : ViewModelBase<MessageRepository>
    {
        private readonly MessageRepository _repository;

        public RepositoryViewModel(MessageRepository repository)
        {
            _repository = repository;
        }

        public string Name
        {
            get { return _repository.Name; }
        }

        public override MessageRepository Model
        {
            get { return _repository; }
        }

        private ObservableCollection<FolderViewModel> _folders;
        public ObservableCollection<FolderViewModel> Folders
        {
            get
            {
                if (_folders == null)
                {
                    InitFolders();
                }
                return _folders;
            }
        }

        private void InitFolders()
        {
            var folders = from f in _repository.Folders
                          select new FolderViewModel(f);
            _folders = new ObservableCollection<FolderViewModel>(folders);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(() => IsSelected);
                if (_isSelected)
                    App.Current.ViewModel.SelectedItem = this;
            }
        }

        public int Merge(MessageRepository rep)
        {
            int n = _repository.Merge(rep);
            InitFolders();
            OnPropertyChanged(() => Folders);
            return n;
        }

        public void Save(string filename)
        {
            _repository.Save(filename);
        }
    }
}
