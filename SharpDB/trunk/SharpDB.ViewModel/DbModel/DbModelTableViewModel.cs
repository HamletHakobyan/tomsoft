using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;

namespace SharpDB.ViewModel.DbModel
{
    public class DbModelTableViewModel : DbModelItemViewModel
    {
        public DbModelTableViewModel(DatabaseViewModel database, IDbTableItem item)
            : base(database, item)
        {
        }

        protected override System.Windows.Media.ImageSource GetImageInternal()
        {
            return GetImageByName("table.png");
        }
    }
}
