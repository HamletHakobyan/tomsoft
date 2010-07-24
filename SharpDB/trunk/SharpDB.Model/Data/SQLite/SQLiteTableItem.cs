using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteTableItem : IDbTableItem
    {
        private SQLiteDbModel.TablesRow _dataRow;
        private IDbItemGroup _group;
        private IDbItemGroup[] _itemGroups;

        public SQLiteTableItem(IDbItemGroup group, SQLiteDbModel.TablesRow tableRow)
        {
            this._group = group;
            this._dataRow = tableRow;
            _itemGroups = new IDbItemGroup[]
            {
                new SQLiteColumnsItemGroup(this),
                new SQLiteIndexesItemGroup(this)
            };
        }

        public IDbItemGroup Group
        {
            get { return _group; }
        }

        public string Name
        {
            get { return _dataRow.TABLE_NAME; }
        }

        public IDbItemGroup[] ItemGroups
        {
            get { return _itemGroups; }
        }

        public SQLiteDbModel.TablesRow DataRow
        {
            get { return _dataRow; }
        }



        public DbItemType ItemType
        {
            get { return DbItemType.Table; }
        }

        public string CustomImageKey
        {
            get { return null; }
        }
    }
}
