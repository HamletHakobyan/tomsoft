using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SharpDB.ViewModel.Design
{
    public class DesignConnectionDialogViewModel
    {
        public DesignConnectionDialogViewModel()
        {
            Name = "MyDataBase";
            ProviderName = "System.Data.SQLite";
            ConnectionString = @"Data Source=C:\foo\bar.db";
            DbProviders = DbProviderFactories.GetFactoryClasses();
        }

        public string Name { get; set; }
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }
        public DataTable DbProviders { get; set; }
    }
}
