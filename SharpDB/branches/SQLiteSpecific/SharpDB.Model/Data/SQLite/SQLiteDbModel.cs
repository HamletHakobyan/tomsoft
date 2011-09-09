using System;
using System.Data.Common;
using System.Data;

namespace SharpDB.Model.Data.SQLite
{
    partial class SQLiteDbModel : IDbModel
    {
        private bool _schemaInitialized;
        private IDbItemGroup[] _itemGroups;

        public void InitModel(DbConnection connection)
        {
            if (!connection.State.HasFlag(ConnectionState.Open))
            {
                connection.Open();
            }

            foreach (DataTable table in this.Tables)
            {
                var schemaTable = connection.GetSchema(table.TableName);
                table.Merge(schemaTable);
            }

            _itemGroups = new IDbItemGroup[]
            {
                new SQLiteTablesItemGroup(this._Tables)
            };

            _schemaInitialized = true;
        }

        bool IDbModel.IsInitialized
        {
            get { return _schemaInitialized; }
        }


        public IDbItemGroup[] ItemGroups
        {
            get { return _itemGroups; }
        }

        public string ConnectionStringEditorTypeName
        {
            get { return null; }
        }
    }
}
