using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using FlickrNet;

namespace FlickIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //CreateDummyAccount();
            LoadConfig();
            Sessions = new Dictionary<string, FlickrSession>();
            Tasks = new ObservableQueue<ITask>();
        }

        private void CreateDummyAccount()
        {
            FlickrAccount account = new FlickrAccount()
            {
                Name = "tom103flickr",
                ApiKey = "2421292d2d709bcca6fdaf677d178121",
                ApiSecret = "",
                LastApiToken = null
            };
            Config = new Config();
            Config.Accounts.Add(account);
            
            SaveConfig();
        }

        new public static App Current
        {
            get { return Application.Current as App; }
        }

        private FlickrAccount _currentAccount;
        public FlickrAccount CurrentAccount
        {
            get
            {
                if (_currentAccount == null)
                {
                    _currentAccount = Config.Accounts.Where(a => a.Name == Config.CurrentAccountName).FirstOrDefault();
                    if (_currentAccount == null)
                    {
                        if (Config.Accounts.Count > 0)
                            _currentAccount = Config.Accounts[0];
                    }
                }
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                Config.CurrentAccountName = (_currentAccount != null) ? _currentAccount.Name : null;
            }
        }

        #region Configuration

        private string _appDataPath;
        public string AppDataPath
        {
            get
            {
                if (_appDataPath == null)
                {
                    _appDataPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        Assembly.GetEntryAssembly().GetName().Name);
                    if (!Directory.Exists(_appDataPath))
                        Directory.CreateDirectory(_appDataPath);
                }
                return _appDataPath;
            }
        }

        public Config Config { get; private set; }

        public void LoadConfig()
        {
            string fileName = Path.Combine(AppDataPath, "config.xml");
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            using (XmlReader rd = XmlReader.Create(fileName))
            {
                Config config = xs.Deserialize(rd) as Config;
                this.Config = config;
            }
        }

        public void SaveConfig()
        {
            string fileName = Path.Combine(AppDataPath, "config.xml");
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter wr = XmlWriter.Create(fileName, settings))
            {
                xs.Serialize(wr, this.Config);
            }
        }

        #endregion

        #region Flickr connection

        public Dictionary<string, FlickrSession> Sessions { get; private set; }

        public FlickrSession CurrentSession
        {
            get
            {
                if (CurrentAccount != null)
                {
                    return GetSession(CurrentAccount);
                }
                else return null;
            }
        }

        public FlickrSession GetSession(FlickrAccount account)
        {
            if (!Sessions.ContainsKey(account.ApiKey))
            {
                Sessions.Add(
                    account.ApiKey,
                    new FlickrSession(account, ShowAuthenticationPage));
            }
            return Sessions[CurrentAccount.ApiKey];
        }

        #endregion

        #region Task queue

        public ObservableQueue<ITask> Tasks { get; private set; }

        #endregion

        private bool? ShowAuthenticationPage(string flickrUrl)
        {
            LoginWindow w = new LoginWindow();
            w.Uri = new Uri(flickrUrl);
            return w.ShowDialog();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveConfig();
        }
    }
}
