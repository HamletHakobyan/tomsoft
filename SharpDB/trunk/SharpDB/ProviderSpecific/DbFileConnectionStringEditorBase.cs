using System.Data.Common;
using System.IO;
using Microsoft.Win32;
using SharpDB.Model.Data;

namespace SharpDB.ProviderSpecific
{
    abstract class DbFileConnectionStringEditorBase : IConnectionStringEditor
    {
        private string _defaultName;

        public string ConnectionString { get; set; }

        public bool? ShowDialog()
        {
            var builder = GetConnectionStringBuilder();
            if (!string.IsNullOrEmpty(ConnectionString))
                builder.ConnectionString = ConnectionString;

            object oDataSource;
            string dataSource = null;
            if (builder.TryGetValue("Data Source", out oDataSource))
                dataSource = oDataSource as string;


            var dlg = new OpenFileDialog();
            dlg.Filter = FileFilter;
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

        public virtual string GetDefaultName()
        {
            return _defaultName;
        }

        protected virtual DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            var factory = DbProviderFactories.GetFactory(ProviderName);
            return factory.CreateConnectionStringBuilder();
        }

        protected abstract string ProviderName { get; }
        protected abstract string FileFilter { get; }

    }
}
