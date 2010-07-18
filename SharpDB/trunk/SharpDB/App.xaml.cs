using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SharpDB.Util;
using SharpDB.Util.Service;
using SharpDB.Service;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using SharpDB.Model;
using System.IO;
using SharpDB.ViewModel;
using System.Windows.Shell;

namespace SharpDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        const string AppGuid = "4CC636DC-90AE-4E05-81E2-44D4F26AFAF1";

        [STAThread]
        public static void Main(string[] args)
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(AppGuid))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        private MainWindowViewModel _mainWindowViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceLocator.Instance.RegisterService(SharpDB.Properties.Resources.ResourceManager);
            ServiceLocator.Instance.RegisterService<ApplicationSettingsBase>(SharpDB.Properties.Settings.Default);
            var config = LoadConfiguration();
            ServiceLocator.Instance.RegisterService(config);

            ServiceLocator.Instance.RegisterService<IDialogService>(new DialogService());
            ServiceLocator.Instance.RegisterService<IMessageBoxService>(new BasicMessageBoxService());
            ServiceLocator.Instance.RegisterService<IFileDialogService>(new FileDialogService());
            ServiceLocator.Instance.RegisterService<IClipboardService>(new ClipboardService());
            ServiceLocator.Instance.RegisterService<IJumpListService>(new JumpListService(config.JumpListItems));

            InitializeSyntaxHighlighting();

            _mainWindowViewModel = new MainWindowViewModel();
            this.MainWindow = new MainWindow { DataContext = _mainWindowViewModel };
            this.MainWindow.Show();
            _mainWindowViewModel.ProcessCommandLineArgs(e.Args);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var config = ServiceLocator.Instance.GetService<Config>();
            var jumpListService = ServiceLocator.Instance.GetService<IJumpListService>();
            config.JumpListItems = jumpListService.GetJumpItems().ToList();
            config.Save();
            SharpDB.Properties.Settings.Default.Save();
            base.OnExit(e);
        }

        private Config LoadConfiguration()
        {
            string configFileName = Config.GetDefaultFileName(SharpDB.Properties.Resources.ApplicationName);
            if (File.Exists(configFileName))
                return Config.FromFile(configFileName);
            return new Config(configFileName);
        }

        private void InitializeSyntaxHighlighting()
        {
            Uri resourceUri = new Uri("/Resources/SQLSyntax.xshd", UriKind.Relative);
            var sri = Application.GetResourceStream(resourceUri);
            if (sri != null)
            {
                using (sri.Stream)
                using (var reader = XmlReader.Create(sri.Stream))
                {
                    var syntaxDefinition = HighlightingLoader.LoadXshd(reader);
                    var highlighting = HighlightingLoader.Load(syntaxDefinition, null);
                    HighlightingManager.Instance.RegisterHighlighting("SQL", new[] { ".sql" }, highlighting);
                }
            }
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            if (_mainWindowViewModel != null)
            {
                _mainWindowViewModel.ProcessCommandLineArgs(args.Skip(1).ToList());
                return true;
            }
            return false;
        }
    }
}
