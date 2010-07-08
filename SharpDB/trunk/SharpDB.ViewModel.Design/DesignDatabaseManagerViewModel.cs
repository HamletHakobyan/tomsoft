using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SharpDB.ViewModel.Design
{
    public class DesignDatabaseManagerViewModel
    {
        public DesignDatabaseManagerViewModel()
        {
            Databases = new ObservableCollection<DesignDatabaseViewModel>
            {
                new DesignDatabaseViewModel { ConnectionName = "Foo" },
                new DesignDatabaseViewModel { ConnectionName = "Bar" }
            };
        }

        public ObservableCollection<DesignDatabaseViewModel> Databases { get; set; }
    }
}
