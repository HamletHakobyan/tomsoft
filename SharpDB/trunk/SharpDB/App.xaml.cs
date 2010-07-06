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

            InitializeSyntaxHighlighting();

            base.OnStartup(e);
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
