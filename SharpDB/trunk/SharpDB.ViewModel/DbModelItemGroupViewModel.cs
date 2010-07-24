using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;
using System.Collections.ObjectModel;

namespace SharpDB.ViewModel
{
    public class DbModelItemGroupViewModel : ViewModelBase
    {
        private IDbItemGroup _group;

        public DbModelItemGroupViewModel(IDbItemGroup group)
        {
            _group = group;
            _name = _group.Name;
        }

        private ObservableCollection<DbModelItemViewModel> _items;
        public ObservableCollection<DbModelItemViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    var items = _group.Items.Select(item => new DbModelItemViewModel(item));
                    _items = new ObservableCollection<DbModelItemViewModel>(items);
                }
                return _items;
            }
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


    }
}
