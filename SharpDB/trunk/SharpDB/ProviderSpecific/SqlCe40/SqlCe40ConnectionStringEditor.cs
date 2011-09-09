namespace SharpDB.ProviderSpecific.SqlCe40
{
    class SqlCe40ConnectionStringEditor : DbFileConnectionStringEditorBase
    {
        protected override string ProviderName
        {
            get { return "System.Data.SqlServerCe.4.0"; }
        }

        protected override string FileFilter
        {
            get { return "SQL Server Compact database files|*.sdf"; }
        }
    }
}
