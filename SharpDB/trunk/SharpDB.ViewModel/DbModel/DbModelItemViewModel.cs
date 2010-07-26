using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpDB.ViewModel.DbModel
{
    public class DbModelItemViewModel : ViewModelBase, IDatabaseChildItem
    {
        private IDbModelItem _item;
        private DatabaseViewModel _database;

        protected DbModelItemViewModel(DatabaseViewModel database, IDbModelItem item)
        {
            _database = database;
            _item = item;
        }

        public string Name
        {
            get { return _item.Name; }
        }

        private ObservableCollection<DbModelItemGroupViewModel> _itemGroups;
        public ObservableCollection<DbModelItemGroupViewModel> ItemGroups
        {
            get
            {
                if (_itemGroups == null)
                {
                    var groups = _item.ItemGroups.Select(group => new DbModelItemGroupViewModel(_database, group));
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

        public virtual string SummaryText
        {
            get
            {
                return string.Format("{0} ({1})", Name, GetResource<string>("ItemType_" + ItemType));
            }
        }

        public ImageSource Image
        {
            get
            {
                return GetImageInternal();
            }
        }

        protected virtual ImageSource GetImageInternal()
        {
            return null;
        }

        protected ImageSource GetImageByName(string imageName)
        {
            if (IsInDesignMode)
                return null;

            Uri imageUri = new Uri(string.Format("/Images/{0}", imageName), UriKind.Relative);
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = imageUri;
            img.EndInit();
            return img;
        }

        public static DbModelItemViewModel FromItem(DatabaseViewModel database, IDbModelItem item)
        {
            if (item is IDbTableItem)
                return new DbModelTableViewModel(database, (IDbTableItem)item);
            if (item is IDbColumnItem)
                return new DbModelColumnViewModel(database, (IDbColumnItem)item);
            if (item is IDbIndexItem)
                return new DbModelIndexViewModel(database, (IDbIndexItem)item);
            if (item is IDbIndexColumnItem)
                return new DbModelIndexColumnViewModel(database, (IDbIndexColumnItem)item);

            return new DbModelItemViewModel(database, item);
        }

        public DatabaseViewModel Database
        {
            get { return _database; }
        }
    }
}
