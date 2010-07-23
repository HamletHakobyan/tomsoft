using System.Data.Common;
using System.Data;
namespace SharpDB.Model.Data.SQLite
{
    [DbProviderName("System.Data.SQLite")]
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
                table.Merge(connection.GetSchema(table.TableName));
            }
        }

        IDbItemGroup[] IDbModel.ItemGroups
        {
            get { return _itemGroups; }
        }
    }
}
