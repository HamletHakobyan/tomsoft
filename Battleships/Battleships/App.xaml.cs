using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Battleships.Service;
using Battleships.ViewModel;

namespace Battleships
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceLocator.Instance.RegisterService<INavigationService>(new MainWindowViewModel());

            base.OnStartup(e);
        }
    }
}
