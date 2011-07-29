using System;
using System.Configuration;
using System.Windows;
using Mediatek.Data;
using Mediatek.Service;
using Mediatek.Service.Implementation;
using Mediatek.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

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
            UnityContainer.RegisterInstance<IFileDialogService>(new FileDialogService());
            UnityContainer.RegisterInstance<IDialogService>(new DialogService());
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
            var unitySection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            unitySection.Configure(unityContainer);
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
    }
}
