using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Velib.Model;
using Velib.Navigation;
using Velib.ViewModel;

namespace Velib
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INavigationService
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

            HomeViewModel viewModel = new HomeViewModel(this);
            this.MainWindow = window;
            window.Navigate(viewModel);
            window.Show();
        }

        public Config Config { get; private set; }

        public string AppDataPath
        {
            get
            {
                string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(userAppData, "Velib");
                return path;
            }
        }
        
        public string ConfigFileName
        {
            get
            {
                string path = Path.Combine(AppDataPath, "config.xml");
                return path;
            }
        }
	

        private void LoadConfig()
        {
            if (File.Exists(ConfigFileName))
            {
                using (StreamReader reader = new StreamReader(ConfigFileName))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Config));
                    try
                    {
                        this.Config = xs.Deserialize(reader) as Config;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading config :\n" + ex.ToString());
                        File.Copy(ConfigFileName, ConfigFileName + ".bak", true);
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
            string dir = Path.GetDirectoryName(ConfigFileName);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (StreamWriter writer = new StreamWriter(ConfigFileName))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                xs.Serialize(writer, Config);
            }
        }

        #region INavigationService Members

        public void Navigate(object content)
        {
            (this.MainWindow as MainWindow).Navigate(content);
        }

        #endregion
    }
}
