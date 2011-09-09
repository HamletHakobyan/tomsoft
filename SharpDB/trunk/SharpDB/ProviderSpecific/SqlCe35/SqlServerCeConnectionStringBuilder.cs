using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SharpDB.ProviderSpecific.SqlCe35
{
    class SqlServerCeConnectionStringBuilder : DbConnectionStringBuilder
    {
        public string DataSource
        {
            get { return (string)this["Data Source"]; }
            set { this["Data Source"] = value; }
        }
    }
}
