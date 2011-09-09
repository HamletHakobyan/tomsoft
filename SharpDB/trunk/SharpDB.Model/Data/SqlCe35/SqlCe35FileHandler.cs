using System.Data.Common;
using System.IO;
using Developpez.Dotnet;

namespace SharpDB.Model.Data.SqlCe35
{
    class SqlCe35FileHandler : IDbFileHandler
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
            get { return "System.Data.SqlServerCe.3.5"; }
        }

        public string MakeConnectionString(string path)
        {
            return string.Format("Data Source={0}", path);
        }
    }
}
