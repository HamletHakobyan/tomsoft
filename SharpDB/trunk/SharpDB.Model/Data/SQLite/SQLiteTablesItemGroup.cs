using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteTablesItemGroup : IDbItemGroup
    {
        private SQLiteDbModel.TablesDataTable _tables;

        public SQLiteTablesItemGroup(SQLiteDbModel.TablesDataTable tables)
        {
            _tables = tables;
        }

        public string Name
        {
            get { return SQLiteResources.ItemGroup_Tables; }
        }

        private IDbModelItem[] _items;
        private SQLiteTableItem sQLiteTableItem;
        public IDbModelItem[] Items
        {
            get
            {
                if (_items == null)
                {
                    _items = _tables.Select(r => new SQLiteTableItem(this, r)).ToArray();
                }
                return _items;
            }
        }

        public void Refresh()
        {
            _items = null;
        }
    }
}
