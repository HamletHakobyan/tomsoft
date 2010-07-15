using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.ViewModel.Design
{
    public class DesignWorksheetViewModel
    {
        public DesignWorksheetViewModel()
        {
            CurrentDatabase = new DesignDatabaseViewModel
            {
                ConnectionName = "My Database",
                IsConnected = true
            };

            Title = "Untitled 1";
        }

        public DesignDatabaseViewModel CurrentDatabase { get; set; }

        public string Title { get; set; }
    }
}
