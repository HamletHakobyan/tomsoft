
namespace Mediatek.Data.EntityFramework
{
    public class MediatekContextFactory : IEntityRepositoryFactory
    {
        public IEntityRepository GetRepository(string dbProviderName, string dbConnectionString)
        {
            return MediatekContext.GetContext(dbProviderName, dbConnectionString);
        }
    }
}
