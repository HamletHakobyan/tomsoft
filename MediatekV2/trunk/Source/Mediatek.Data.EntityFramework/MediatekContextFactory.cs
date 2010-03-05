using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
