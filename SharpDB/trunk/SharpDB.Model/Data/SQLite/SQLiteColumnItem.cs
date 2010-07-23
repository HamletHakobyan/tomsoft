using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteColumnItem : IDbColumnItem
    {
        private SQLiteColumnsItemGroup _group;
        private SQLiteDbModel.ColumnsRow _dataRow;
        private IDbItemGroup[] _itemGroups;

        public SQLiteColumnItem(SQLiteColumnsItemGroup group, SQLiteDbModel.ColumnsRow columnRow)
        {
            _group = group;
            _dataRow = columnRow;
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
    }
}
