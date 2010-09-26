using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Mediatek.Service;
using Mediatek.Service.Implementation;
using Mediatek.ViewModel;
using Mediatek.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Mediatek.Helpers;

namespace Mediatek
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        new public static App Current
        {
            get
            {
                return Application.Current as App;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup services
            UnityContainer = SetupUnityContainerFromConfig();
            UnityContainer.RegisterInstance<IMessageBoxService>(new MessageBoxService());
            UnityContainer.RegisterInstance<IViewModelRepository>(new ViewModelRepository());
            var mainVM = new MainViewModel();
            UnityContainer.RegisterInstance<INavigationService>(mainVM);

            // Setup and show main window
            var mainWindow = new MainWindow { DataContext = mainVM };
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public static IUnityContainer UnityContainer { get; private set; }

        public static T GetService<T>()
        {
            return UnityContainer.Resolve<T>();
        }

        private static IUnityContainer SetupUnityContainerFromConfig()
        {
            var unityContainer = new UnityContainer();
            var unitySection = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            var containerElement = unitySection.Containers.Default;
            containerElement.Configure(unityContainer);
            return unityContainer;
        }

        private static IEntityRepository _repository;
        public static IEntityRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    var setting = ConfigurationManager.ConnectionStrings["MediatekDb"];
                    var factory = UnityContainer.Resolve<IEntityRepositoryFactory>();
                    _repository = factory.GetRepository(setting.ProviderName, setting.ConnectionString);
                }
                return _repository;
            }
        }

        private Uri _loadingImageUri;
        public Uri LoadingImageUri
        {
            get
            {
                if (_loadingImageUri == null)
                {
                    _loadingImageUri = new Uri("pack://application:,,,/Images/loading.gif");
                }
                return _loadingImageUri;
            }
        }
    }
}
