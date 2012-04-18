using System;
using System.Text;
using System.Net;
using System.IO;
using Developpez.Dotnet.Text;
using Developpez.Dotnet.IO;

namespace VBulletinBox.Models
{
    public class VBulletinClient
    {
        private readonly VBulletinAccount _account;
        public VBulletinClient(VBulletinAccount account)
        {
            this._account = account;
        }

        public bool LoggedIn { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
        public string StatusDescription { get; set; }
        public CookieCollection Cookies { get; set; }

        private static readonly StringTemplate _loginTemplate = "do=login&s=&cookieuser=1&vb_login_username={UserName}&vb_login_password=&vb_login_md5password={PasswordMD5}";

        public bool Login()
        {
            this.LoggedIn = false;
            this.Cookies = null;

            string loginData = _loginTemplate.Format(new { _account.UserName, _account.PasswordMD5 });
            Uri loginUri = new Uri(_account.SiteUrl.Trim('/') + "/login.php");
            ServicePoint servicePoint = ServicePointManager.FindServicePoint(loginUri);
            servicePoint.Expect100Continue = false;
            HttpWebRequest req = HttpWebRequest.Create(loginUri) as HttpWebRequest;
            req.Method = "POST";
            req.CookieContainer = new CookieContainer();
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = Encoding.ASCII.GetBytes(loginData);
            req.ContentLength = buffer.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
            }
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp.Cookies["bbuserid"] != null)
                {
                    this.Cookies = resp.Cookies;
                    this.LoggedIn = true;
                }
                else
                {
                    throw new Exception("Login error: missing cookie");
                }
                this.StatusCode = resp.StatusCode;
                this.StatusDescription = resp.StatusDescription;
            }

            return this.LoggedIn;
        }

        public MessageRepository GetMessages()
        {
            if (!this.LoggedIn)
                Login();

            Uri messagesUri = new Uri(_account.SiteUrl.Trim('/') + "/private.php?do=downloadpm&dowhat=xml");
            HttpWebRequest req = HttpWebRequest.Create(messagesUri) as HttpWebRequest;
            req.Method = "GET";
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(this.Cookies);
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                using (Stream stream = resp.GetResponseStream())
                {
                    string path = Path.GetTempFileName();
                    stream.CopyToFile(path);
                    return MessageRepository.FromFile(path, _account);
                }
            }
        }
    }
}
