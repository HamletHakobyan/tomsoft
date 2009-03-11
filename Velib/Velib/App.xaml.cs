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
            LoadRepository();

            Velib.MainWindow window = new MainWindow();

            Velib.ViewModel.NetworkRepositoryViewModel viewModel = new Velib.ViewModel.NetworkRepositoryViewModel(window, Repository);
            Velib.View.NetworkRepositoryView view = new Velib.View.NetworkRepositoryView();
            view.DataContext = viewModel;

            this.MainWindow = window;
            window.Navigate(view);
            window.Show();
        }

        public NetworkRepository Repository { get; private set; }

        public string RepositoryPath
        {
            get
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appData, @"Velib\networkRepository.xml");
                return path;
            }
        }
	

        private void LoadRepository()
        {
            if (File.Exists(RepositoryPath))
            {
                using (StreamReader reader = new StreamReader(RepositoryPath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(NetworkRepository));
                    try
                    {
                        this.Repository = xs.Deserialize(reader) as NetworkRepository;
                        foreach (Network network in this.Repository.Networks)
                        {
                            new VelibProvider(network);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading network repository :\n" + ex.ToString());
                        File.Copy(RepositoryPath, RepositoryPath + ".bak", true);
                    }
                }
            }
            if (this.Repository == null)
            {
                this.Repository = new NetworkRepository();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Repository != null)
            {
                SaveRepository();
            }
        }

        private void SaveRepository()
        {
            string dir = Path.GetDirectoryName(RepositoryPath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (StreamWriter writer = new StreamWriter(RepositoryPath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(NetworkRepository));
                xs.Serialize(writer, Repository);
            }
        }
    }
}
