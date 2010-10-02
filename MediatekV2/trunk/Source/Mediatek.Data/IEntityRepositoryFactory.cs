
namespace Mediatek.Data
{
    public interface IEntityRepositoryFactory
    {
        IEntityRepository GetRepository(string dbProviderName, string dbConnectionString);
    }
}
