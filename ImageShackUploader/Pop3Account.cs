using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageShackUploader
{
    public class Pop3Account
    {
        public Pop3Account()
        {
            Port = 110;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
