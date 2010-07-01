using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SharpDB.Util;
using SharpDB.Util.Dialogs;
using SharpDB.Service;

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

            base.OnStartup(e);
        }
    }
}
