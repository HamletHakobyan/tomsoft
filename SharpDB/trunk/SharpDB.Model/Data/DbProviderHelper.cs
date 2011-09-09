using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Reflection;

namespace SharpDB.Model.Data
{
    public static class DbProviderHelper
    {
        private static readonly ConcurrentDictionary<string, Type> _dbModelCache;
        private static readonly ConcurrentDictionary<string, Type> _connectionStringEditorCache;
        private static readonly ConcurrentDictionary<string, Type> _dbFileHandlerCache;

        static DbProviderHelper()
        {
            _dbModelCache = new ConcurrentDictionary<string, Type>();
            _connectionStringEditorCache = new ConcurrentDictionary<string, Type>();
            _dbFileHandlerCache = new ConcurrentDictionary<string, Type>();
        }

        public static IDbModel GetDbModel(string providerName)
        {
            Type implementation = _dbModelCache.GetOrAdd(providerName, FindProviderSpecificImplementation<IDbModel>);
            if (implementation == null)
                return null;

            return (IDbModel)Activator.CreateInstance(implementation);
        }

        public static IConnectionStringEditor GetConnectionStringEditor(string providerName)
        {
            Type implementation = _connectionStringEditorCache.GetOrAdd(providerName, FindProviderSpecificImplementation<IConnectionStringEditor>);
            if (implementation == null)
                return null;

            return (IConnectionStringEditor)Activator.CreateInstance(implementation);
        }

        public static IDbFileHandler GetFileHandler(string providerName)
        {
            Type implementation = _dbFileHandlerCache.GetOrAdd(providerName, FindProviderSpecificImplementation<IDbFileHandler>);
            if (implementation == null)
                return null;

            return (IDbFileHandler)Activator.CreateInstance(implementation);
        }

        private static Type FindProviderSpecificImplementation<T>(string providerName)
        {
            // TODO search for third party implementations in other assemblies

            var types = from asm in AppDomain.CurrentDomain.GetAssemblies()
                        from attr in asm.GetAttributes<DbProviderSpecificTypeAttribute>()
                        where string.Equals(attr.ProviderName, providerName, StringComparison.InvariantCultureIgnoreCase)
                        && !attr.Type.IsInterface
                        && !attr.Type.IsAbstract
                        && attr.Type.Is<T>()
                        select attr.Type;

            return types.FirstOrDefault();
        }
    }
}
