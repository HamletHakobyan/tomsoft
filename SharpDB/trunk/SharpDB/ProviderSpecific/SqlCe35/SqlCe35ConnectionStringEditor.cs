namespace SharpDB.ProviderSpecific.SqlCe35
{
    class SqlCe35ConnectionStringEditor : DbFileConnectionStringEditorBase
    {
        protected override string ProviderName
        {
            get { return "System.Data.SqlServerCe.3.5"; }
        }

        protected override string FileFilter
        {
            get { return "SQL Server Compact database files|*.sdf"; }
        }

        protected override System.Data.Common.DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new SqlServerCeConnectionStringBuilder();
        }
    }
}
