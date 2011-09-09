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
            var ds = new DataSet();
            
            var catalogs = connection.GetSchema("Catalogs");
            var tables = connection.GetSchema("Tables");
            var columns = connection.GetSchema("Columns");
            var indexes = connection.GetSchema("Indexes");
            var indexColumns = connection.GetSchema("IndexColumns");
            var views = connection.GetSchema("Views");
            var viewColumns = connection.GetSchema("ViewColumns");
            var foreignKeys = connection.GetSchema("ForeignKeys");
            var triggers = connection.GetSchema("Triggers");

            ds.Tables.Add(catalogs);
            ds.Tables.Add(tables);
            ds.Tables.Add(columns);
            ds.Tables.Add(indexes);
            ds.Tables.Add(indexColumns);
            ds.Tables.Add(views);
            ds.Tables.Add(viewColumns);
            ds.Tables.Add(foreignKeys);
            ds.Tables.Add(triggers);



            return ds;
        }
    }
}
