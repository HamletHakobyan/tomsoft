using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;
using System.Collections.ObjectModel;

namespace SharpDB.ViewModel.DbModel
{
    public class DbModelItemGroupViewModel : ViewModelBase, IDatabaseChildItem
    {
        private IDbItemGroup _group;
        private DatabaseViewModel _database;

        public DbModelItemGroupViewModel(DatabaseViewModel database, IDbItemGroup group)
        {
            _database = database;
            _group = group;
        }

        private ObservableCollection<DbModelItemViewModel> _items;
        public ObservableCollection<DbModelItemViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    var items = _group.Items.Select(item => DbModelItemViewModel.FromItem(_database, item)).OrderBy(item => item.Name);
                    _items = new ObservableCollection<DbModelItemViewModel>(items);
                }
                return _items;
            }
        }

        public string Name
        {
            get { return _group.Name; }
        }

        public DatabaseViewModel Database
        {
            get { return _database; }
        }
    }
}
