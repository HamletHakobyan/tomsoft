using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageShackUploader
{
    public class ImageShackAccount
    {
        public ImageShackAccount()
        {
            Smtp = new SmtpAccount();
            Pop3 = new Pop3Account();
        }

        public string Name { get; set; }
        public string RegistrationCode { get; set; }
        public string Email { get; set; }
        public SmtpAccount Smtp { get; set; }
        public Pop3Account Pop3 { get; set; }
    }
}
