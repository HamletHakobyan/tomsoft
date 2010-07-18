using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SharpDB.Model.Data
{
    public class DbSchema
    {
        private DataSet _schemaDataSet;

        public DbSchema(DbConnection connection)
        {
            _schemaDataSet = BuildSchemaDataSet(connection);
        }

        private DataSet BuildSchemaDataSet(DbConnection connection)
        {
            DataSet ds = new DataSet();
            DataTable metadataCollections = connection.GetSchema();
            foreach (DataRow row in metadataCollections.Rows)
            {
                string collectionName = (string)row["CollectionName"];
                DataTable dt = connection.GetSchema(collectionName);
                dt.TableName = collectionName;
                ds.Tables.Add(dt);
            }
            return ds;
        }
    }
}
