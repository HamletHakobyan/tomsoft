using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Reflection;

namespace SharpDB.Model.Data
{
    public static class DbModelHelper
    {
        public static IDbModel GetModel(string providerName)
        {
            var types = typeof(DbModelHelper).Assembly.GetTypes()
                        .Where(t => !t.IsInterface &&
                                    !t.IsAbstract &&
                                    t.Is<IDbModel>())
                        .ToList();

            // TODO load third party model implementation

            var candidates =
                from t in types
                let a = t.GetAttribute<DbProviderNameAttribute>()
                where a != null
                && a.Name == providerName
                select t;

            Type implementation = candidates.FirstOrDefault();
            if (implementation == null)
                return null;

            return (IDbModel)Activator.CreateInstance(implementation);
        }
    }
}
