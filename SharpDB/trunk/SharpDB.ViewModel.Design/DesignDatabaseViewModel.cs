using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SharpDB.ViewModel.Design
{
    public class DesignDatabaseViewModel
    {
        public DesignDatabaseViewModel()
        {
            ModelGroups = new ObservableCollection<object>
            {
                "Tables",
                "Indexes"
            };
        }

        public string ConnectionName { get; set; }
        public bool IsConnected { get; set; }
        public bool IsBusy { get; set; }

        public ObservableCollection<object> ModelGroups { get; set; }
    }
}
