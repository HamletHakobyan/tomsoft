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

namespace ImageShackUploader
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
        }

        private void CreateDummyAccount()
        {
            SmtpAccount smtp = new SmtpAccount
            {
                Host = "smtp.free.fr"
            };

            Pop3Account pop3 = new Pop3Account
            {
                Host = "pop.free.fr",
                Username = "tom103imageshack",
                Password = "pipopipo"
            };

            ImageShackAccount account = new ImageShackAccount
            {
                RegistrationCode = "40e6c32929a2bc07f1427de51d4485a7",
                Smtp = smtp,
                Pop3 = pop3
            };

            Config = new Config();
            Config.Accounts.Add(account);

            SaveConfig();
        }

        new public static App Current
        {
            get { return Application.Current as App; }
        }

        private ImageShackAccount _currentAccount;
        public ImageShackAccount CurrentAccount
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


        public void SendImage(BitmapSource imageSource)
        {
            ImageShackAccount acc = CurrentAccount;
            SmtpClient client = new SmtpClient();
            client.Host = acc.Smtp.Host;
            client.Port = acc.Smtp.Port;
            client.EnableSsl = acc.Smtp.EnableSSL;
            if (!string.IsNullOrEmpty(acc.Smtp.Username))
            {
                client.Credentials = new NetworkCredential(acc.Smtp.Username, acc.Smtp.Password);
            }
            string tmp = Path.GetTempFileName();
            FileInfo fi = new FileInfo(tmp);
            string filename = fi.FullName.Replace(fi.Extension, ".png");
            File.Move(tmp, filename);
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageSource));
                encoder.Save(fs);
            }

            MailMessage msg = new MailMessage(acc.Email, "image@imageshack.us", acc.RegistrationCode, "");
            msg.ReplyTo = new MailAddress(acc.Email);
            msg.Attachments.Add(new Attachment(filename));

            client.Send(msg);
        }
    }
}
