using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteIndexItem : IDbIndexItem
    {
        private SQLiteIndexesItemGroup _group;
        private SQLiteDbModel.IndexesRow _dataRow;
        private IDbItemGroup[] _itemGroups;

        public SQLiteIndexItem(SQLiteIndexesItemGroup group, SQLiteDbModel.IndexesRow indexRow)
        {
            _group = group;
            _dataRow = indexRow;
            // TODO : add index columns
            _itemGroups = new IDbItemGroup[0];
        }

        public IDbItemGroup Group
        {
            get { return _group; }
        }

        public string Name
        {
            get { return _dataRow.INDEX_NAME; }
        }

        public IDbItemGroup[] ItemGroups
        {
            get { return _itemGroups; }
        }



        public DbItemType ItemType
        {
            get { return DbItemType.Index; }
        }

        public string CustomImageKey
        {
            get { return null; }
        }
    }
}
