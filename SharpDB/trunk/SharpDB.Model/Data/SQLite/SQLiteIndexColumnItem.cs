using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteIndexColumnItem : IDbIndexColumnItem
    {
        private SQLiteIndexColumnItemGroup _group;
        private SQLiteDbModel.IndexColumnsRow _dataRow;
        private IDbItemGroup[] _itemGroups;

        public SQLiteIndexColumnItem(SQLiteIndexColumnItemGroup group, SQLiteDbModel.IndexColumnsRow indexColumnRow)
        {
            _group = group;
            _dataRow = indexColumnRow;
            _itemGroups = new IDbItemGroup[0];
        }

        public IDbItemGroup Group
        {
            get { return _group; }
        }

        public string Name
        {
            get { return _dataRow.COLUMN_NAME; }
        }

        public IDbItemGroup[] ItemGroups
        {
            get { return _itemGroups; }
        }

        public DbItemType ItemType
        {
            get { return DbItemType.Column; }
        }

        public string CustomImageKey
        {
            get { return null; }
        }

        public string Description
        {
            get { return null; }
        }
    }
}
