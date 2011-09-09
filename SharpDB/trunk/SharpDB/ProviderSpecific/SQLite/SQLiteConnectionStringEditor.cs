namespace SharpDB.ProviderSpecific.SQLite
{
    class SQLiteConnectionStringEditor : DbFileConnectionStringEditorBase
    {
        protected override string ProviderName
        {
            get { return "System.Data.SQLite"; }
        }

        protected override string FileFilter
        {
            get { return "SQLite database files|*.db;*.sqlite"; }
        }
    }
}
