using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using SharpDB.Model.Data;

namespace SharpDB.ProviderSpecific.SQLite
{
    class SQLiteConnectionStringEditor : IConnectionStringEditor
    {
        private string _defaultName;

        public string ConnectionString { get; set; }
        public bool? ShowDialog()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SQLite");
            var builder = factory.CreateConnectionStringBuilder();
            if (!string.IsNullOrEmpty(ConnectionString))
                builder.ConnectionString = ConnectionString;

            object oDataSource;
            string dataSource = null;
            if (builder.TryGetValue("Data Source", out oDataSource))
                dataSource = oDataSource as string;


            var dlg = new OpenFileDialog();
            dlg.Filter = "SQLite database files|*.*";
            dlg.FileName = dataSource;
            if (dlg.ShowDialog() == true)
            {
                builder["Data Source"] = dlg.FileName;
                ConnectionString = builder.ConnectionString;
                _defaultName = Path.GetFileNameWithoutExtension(dlg.FileName);
                return true;
            }
            ConnectionString = string.Empty;
            return false;
        }

        public string GetDefaultName()
        {
            return _defaultName;
        }
    }
}
