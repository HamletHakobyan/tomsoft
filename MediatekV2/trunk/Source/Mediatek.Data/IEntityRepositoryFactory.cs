using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Data
{
    public interface IEntityRepositoryFactory
    {
        IEntityRepository GetRepository(string dbProviderName, string dbConnectionString);
    }
}
