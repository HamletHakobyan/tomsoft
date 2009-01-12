using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ImageShackUploader
{
    public class Config
    {
        public Config()
        {
            Accounts = new List<ImageShackAccount>();
        }

        public List<ImageShackAccount> Accounts { get; set; }
        
        [XmlElement("CurrentAccount")]
        public string CurrentAccountName { get; set; }
    }
}
