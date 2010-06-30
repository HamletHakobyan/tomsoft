using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util
{
    public class ServiceLocator : IServiceProvider
    {
        #region Private data

        private readonly Dictionary<Type, Dictionary<string, object>> _services;

        #endregion

        #region Constructor

        protected ServiceLocator()
        {
            _services = new Dictionary<Type, Dictionary<string, object>>();
        }

        #endregion

        #region Singleton implementation

        private readonly static ServiceLocator _instance;

        static ServiceLocator()
        {
            _instance = new ServiceLocator();
        }

        public static ServiceLocator Instance
        {
            get { return _instance; }
        }

        #endregion

        #region RegisterService methods

        public void RegisterService(Type serviceType, string name, object service)
        {
            lock (_services)
            {
                var services = GetServicesForType(serviceType, true);
                services.Add(name ?? string.Empty, service);
            }
        }

        public void RegisterService(Type serviceType, object service)
        {
            RegisterService(serviceType, string.Empty, service);
        }

        public void RegisterService<T>(string name, object service)
        {
            RegisterService(typeof(T), name, service);
        }

        public void RegisterService<T>(object service)
        {
            RegisterService(typeof(T), string.Empty, service);
        }

        #endregion

        #region GetService methods

        public object GetService(Type serviceType, string name)
        {
            var service = GetServiceInternal(serviceType, name);
            if (service == null)
                throw new KeyNotFoundException(string.Format("No service registered for type '{0}' and name '{1}'", serviceType.FullName, name));
            return service;
        }

        public object GetService(Type serviceType)
        {
            return GetService(serviceType, string.Empty);
        }

        public T GetService<T>(string name)
        {
            return (T)GetService(typeof(T), name);
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T), string.Empty);
        }

        // Explicit IServiceProvider implementation should return null
        // if service is not found, not throw an exception
        object IServiceProvider.GetService(Type serviceType)
        {
            return GetServiceInternal(serviceType, string.Empty);
        }

        #endregion

        #region Private methods

        private Dictionary<string, object> GetServicesForType(Type serviceType, bool create)
        {
            lock (_services)
            {
                Dictionary<string, object> services;
                if (!_services.TryGetValue(serviceType, out services))
                {
                    if (create)
                    {
                        services = new Dictionary<string, object>();
                        _services[serviceType] = services;
                    }
                }
                return services;
            }
        }

        private object GetServiceInternal(Type serviceType, string name)
        {
            lock (_services)
            {
                var services = GetServicesForType(serviceType, false);
                if (services != null)
                {
                    object service;
                    services.TryGetValue(name ?? string.Empty, out service);
                    return service;
                }
                return null;
            }
        }

        #endregion
    }
}
