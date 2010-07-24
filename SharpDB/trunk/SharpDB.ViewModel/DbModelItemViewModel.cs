using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;
using System.Collections.ObjectModel;

namespace SharpDB.ViewModel
{
    public class DbModelItemViewModel : ViewModelBase
    {
        private IDbModelItem _item;

        public DbModelItemViewModel(IDbModelItem item)
        {
            _item = item;
            _name = _item.Name;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private ObservableCollection<DbModelItemGroupViewModel> _itemGroups;
        public ObservableCollection<DbModelItemGroupViewModel> ItemGroups
        {
            get
            {
                if (_itemGroups == null)
                {
                    var groups = _item.ItemGroups.Select(group => new DbModelItemGroupViewModel(group));
                    _itemGroups = new ObservableCollection<DbModelItemGroupViewModel>(groups);
                }
                return _itemGroups;
            }
        }

        public DbItemType ItemType
        {
            get { return _item.ItemType; }
        }

        public string CustomImageKey
        {
            get { return _item.CustomImageKey; }
        }

    }
}
