using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;

namespace SharpDB.ViewModel.DbModel
{
    public class DbModelIndexColumnViewModel : DbModelItemViewModel
    {
        public DbModelIndexColumnViewModel(DatabaseViewModel database, IDbIndexColumnItem item)
            : base(database, item)
        {
        }

        protected override System.Windows.Media.ImageSource GetImageInternal()
        {
            return GetImageByName("column.png");
        }
    }
}
