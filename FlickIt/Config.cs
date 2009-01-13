using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace FlickIt
{
    public class Config
    {
        public Config()
        {
            Accounts = new ObservableCollection<FlickrAccount>();
        }

        public ObservableCollection<FlickrAccount> Accounts { get; set; }

        [XmlElement("CurrentAccount")]
        public string CurrentAccountName { get; set; }
    }
}
