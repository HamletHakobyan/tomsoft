using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteFileHandler : IDbFileHandler
    {
        public bool CanUseFile(string path)
        {
            string extension = Path.GetExtension(path);
            if (extension != null)
                extension = extension.ToLower();
            return extension.In(".db", ".sqlite");
        }

        public string ProviderName
        {
            get { return "System.Data.SQLite"; }
        }

        public string MakeConnectionString(string path)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
            DbConnectionStringBuilder builder = factory.CreateConnectionStringBuilder();
            builder["Data Source"] = path;
            return builder.ConnectionString;
        }
    }
}
