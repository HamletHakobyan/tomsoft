using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteIndexesItemGroup : IDbItemGroup
    {
        private SQLiteTableItem _table;
        private IDbModelItem[] _items;

        public SQLiteIndexesItemGroup(SQLiteTableItem table)
        {
            this._table = table;
        }

        public SQLiteTableItem Table
        {
            get { return _table; }
        }

        public string Name
        {
            get { return SQLiteResources.ItemGroup_Indexes; }
        }

        public IDbModelItem[] Items
        {
            get {
                if (_items == null)
                {
                    _items = _table.DataRow.GetIndexesRows().Select(r => new SQLiteIndexItem(this, r)).ToArray();
                }
                return _items; }
        }

        public void Refresh()
        {
            _items = null;
        }
    }
}
