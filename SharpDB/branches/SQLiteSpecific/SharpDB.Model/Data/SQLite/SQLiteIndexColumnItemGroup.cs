using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteIndexColumnItemGroup : IDbItemGroup
    {
        private SQLiteIndexItem _index;
        private IDbModelItem[] _items;

        public SQLiteIndexColumnItemGroup(SQLiteIndexItem index)
        {
            this._index = index;
        }

        public SQLiteIndexItem Index
        {
            get { return _index; }
        }

        public string Name
        {
            get { return SQLiteResources.ItemGroup_Columns; }
        }

        public IDbModelItem[] Items
        {
            get
            {
                if (_items == null)
                {
                    _items = _index.DataRow.GetIndexColumnsRows().Select(r => new SQLiteIndexColumnItem(this, r)).ToArray();
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
