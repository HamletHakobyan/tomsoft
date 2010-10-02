using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mediatek.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Mediatek.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Index",                                                // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
        }

        public static IUnityContainer UnityContainer
        {
            get
            {
                var container = HttpContext.Current.Items["unityContainer"] as IUnityContainer;
                if (container == null)
                {
                    container = new UnityContainer();
                    var unitySection = (UnityConfigurationSection)HttpContext.Current.GetSection("unity");
                    var containerElement = unitySection.Containers.Default;
                    containerElement.Configure(container);

                    HttpContext.Current.Items["unityContainer"] = container;
                }
                return container;
            }
        }

        public static IEntityRepository GetRepository()
        {
            var repository = HttpContext.Current.Items["entityRepository"] as IEntityRepository;
            if (repository == null)
            {
                var setting = ConfigurationManager.ConnectionStrings["MediatekDb"];
                var factory = UnityContainer.Resolve<IEntityRepositoryFactory>();
                repository = factory.GetRepository(setting.ProviderName, setting.ConnectionString);
                HttpContext.Current.Items["entityRepository"] = repository;
            }
            return repository;
        }


    }
}