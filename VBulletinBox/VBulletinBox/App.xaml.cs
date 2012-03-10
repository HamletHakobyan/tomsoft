using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Windows.Markup;
using System.Globalization;

namespace VBulletinBox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Fix internationalization of bindings
            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                  XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));

            // Create the ViewModel and expose it using the View's DataContext
            Views.MainView view = new Views.MainView();
            this.ViewModel = new ViewModels.MainViewModel();
            view.DataContext = this.ViewModel;
            view.Show();
        }

        public ViewModels.MainViewModel ViewModel { get; set; }

        public new static App Current
        {
            get { return Application.Current as App; }
        }
    }
}
