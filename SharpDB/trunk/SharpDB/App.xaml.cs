using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SharpDB.Util;
using SharpDB.Util.Dialogs;
using SharpDB.Service;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using SharpDB.Model;
using System.IO;

namespace SharpDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceLocator.Instance.RegisterService<IDialogService>(new DialogService());
            ServiceLocator.Instance.RegisterService<IMessageBoxService>(new BasicMessageBoxService());
            ServiceLocator.Instance.RegisterService<IFileDialogService>(new FileDialogService());
            ServiceLocator.Instance.RegisterService(SharpDB.Properties.Resources.ResourceManager);
            ServiceLocator.Instance.RegisterService(GetConfiguration());

            InitializeSyntaxHighlighting();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SharpDB.Properties.Settings.Default.Save();
            base.OnExit(e);
        }

        private Config GetConfiguration()
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
    }
}
