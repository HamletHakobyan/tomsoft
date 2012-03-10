using System;
using System.Linq;
using System.Windows.Data;
using Developpez.Dotnet;
using Developpez.Dotnet.Windows.Collections;
using VBulletinBox.Models;
using System.Collections.ObjectModel;

namespace VBulletinBox.ViewModels
{
    public class FolderViewModel : ViewModelBase<MessageFolder>
    {
        private readonly MessageFolder _folder;

        public FolderViewModel(MessageFolder folder)
        {
            _folder = folder;
        }
        
        public string Name
        {
            get { return _folder.Name; }
            set
            {
                _folder.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        private ObservableCollection<MessageViewModel> _messages;
        public ObservableCollection<MessageViewModel> Messages
        {
            get
            {
                if (_messages == null)
                {
                    var messages = from m in _folder.Messages
                                   select new MessageViewModel(m);
                    _messages = new ObservableCollection<MessageViewModel>(messages);
                }
                return _messages;
            }
        }

        private MessageViewModel _currentMessage;
        public MessageViewModel CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                _currentMessage = value;
                App.Current.ViewModel.CurrentMessage = _currentMessage;
                OnPropertyChanged(() => CurrentMessage);
            }
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

        private string _searchFilter;
        public string SearchFilter
        {
            get { return _searchFilter; }
            set
            {
                _searchFilter = value;
                OnPropertyChanged("SearchFilter");
                UpdateFilter(value);
            }
        }

        private void UpdateFilter(string value)
        {
            if (value.IsNullOrEmpty())
            {
                _messages.ShapeView().ClearFilter();
                return;
            }

            var query =
                from m in _messages.ShapeView()
                where m.Title != null && m.Title.Contains(value, StringComparison.CurrentCultureIgnoreCase)
                || m.FromUser != null && m.FromUser.Contains(value, StringComparison.CurrentCultureIgnoreCase)
                || m.ToUser != null && m.ToUser.Contains(value, StringComparison.CurrentCultureIgnoreCase)
                || m.Body != null && m.Body.Contains(value, StringComparison.CurrentCultureIgnoreCase)
                select m;
            query.Apply();

        }

        public override MessageFolder Model
        {
            get { return _folder; }
        }
    }
}
