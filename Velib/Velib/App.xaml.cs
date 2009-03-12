using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Velib.Model;
using System.IO;
using System.Xml.Serialization;

namespace Velib
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current
        {
            get
            {
                return Application.Current as App;
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoadConfig();

            Velib.MainWindow window = new MainWindow();

            Velib.ViewModel.HomeViewModel viewModel = new Velib.ViewModel.HomeViewModel(window);
            Velib.View.HomeView view = new Velib.View.HomeView();
            view.DataContext = viewModel;

            this.MainWindow = window;
            window.Navigate(view);
            window.Show();
        }

        public Config Config { get; private set; }

        public string ConfigPath
        {
            get
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appData, @"Velib\config.xml");
                return path;
            }
        }
	

        private void LoadConfig()
        {
            if (File.Exists(ConfigPath))
            {
                using (StreamReader reader = new StreamReader(ConfigPath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Config));
                    try
                    {
                        this.Config = xs.Deserialize(reader) as Config;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading config :\n" + ex.ToString());
                        File.Copy(ConfigPath, ConfigPath + ".bak", true);
                    }
                }
            }
            if (this.Config == null)
            {
                this.Config = new Config();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Config != null)
            {
                SaveConfig();
            }
        }

        private void SaveConfig()
        {
            string dir = Path.GetDirectoryName(ConfigPath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (StreamWriter writer = new StreamWriter(ConfigPath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                xs.Serialize(writer, Config);
            }
        }
    }
}
