using System.Data.Common;
using System.IO;
using Developpez.Dotnet;

namespace SharpDB.Model.Data.SqlCe40
{
    class SqlCe40FileHandler : IDbFileHandler
    {
        public bool CanUseFile(string path)
        {
            string extension = Path.GetExtension(path);
            if (extension != null)
                extension = extension.ToLower();
            return extension.In(".sdf");
        }

        public string ProviderName
        {
            get { return "System.Data.SqlServerCe.4.0"; }
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
