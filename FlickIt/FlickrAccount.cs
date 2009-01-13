using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickIt
{
    public class FlickrAccount
    {
        public FlickrAccount()
        {
        }

        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string LastApiToken { get; set; }
    }
}
