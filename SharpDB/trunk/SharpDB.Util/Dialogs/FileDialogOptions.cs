using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Dialogs
{
    public abstract class FileDialogOptions
    {
        public string InitialPath { get; set; }
        public string Title { get; set; }
        public string Filters { get; set; }
        public int SelectedFilter { get; set; }
    }
}
