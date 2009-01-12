using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageShackUploader
{
    public class SmtpAccount
    {
        public SmtpAccount()
        {
            this.Port = 25;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
