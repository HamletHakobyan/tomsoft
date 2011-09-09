using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SharpDB.ViewModel.Design
{
    public class DesignMainWindowViewModel
    {
        public DesignMainWindowViewModel()
        {
            Worksheets = new ObservableCollection<DesignWorksheetViewModel>
            {
                new DesignWorksheetViewModel(),
                new DesignWorksheetViewModel { Title = "Untitled 2" }
            };
            CurrentWorksheet = Worksheets[0];
            DatabaseManager = new DesignDatabaseManagerViewModel();
            Title = "SharpDB - Design";
        }

        public ObservableCollection<DesignWorksheetViewModel> Worksheets { get; set; }
        public DesignWorksheetViewModel CurrentWorksheet { get; set; }
        public DesignDatabaseManagerViewModel DatabaseManager { get; set; }
        public string Title { get; set; }
    }
}
