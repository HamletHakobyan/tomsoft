namespace SharpDB.Model.Data
{
    public interface IDbFileHandler
    {
        bool CanUseFile(string path);
        string ProviderName { get; }
        string MakeConnectionString(string path);
    }
}
