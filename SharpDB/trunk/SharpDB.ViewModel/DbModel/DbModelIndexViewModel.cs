using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;

namespace SharpDB.ViewModel.DbModel
{
    public class DbModelIndexViewModel : DbModelItemViewModel
    {
        private IDbIndexItem _indexItem;

        public DbModelIndexViewModel(DatabaseViewModel database, IDbIndexItem item)
            : base(database, item)
        {
            _indexItem = item;
        }

        protected override System.Windows.Media.ImageSource GetImageInternal()
        {
            if (_indexItem.IsPrimaryKey)
                return GetImageByName("primary_key.png");
            if (_indexItem.IsUnique)
                return GetImageByName("primary_key.png");
            return GetImageByName("index.png");
        }
    }
}
