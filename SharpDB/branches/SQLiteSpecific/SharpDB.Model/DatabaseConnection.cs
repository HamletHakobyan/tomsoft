using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model
{
    public class DatabaseConnection
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}
