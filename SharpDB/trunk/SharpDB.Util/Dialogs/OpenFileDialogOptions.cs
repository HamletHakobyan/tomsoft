using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Dialogs
{
    public class OpenFileDialogOptions : FileDialogOptions
    {
        public OpenFileDialogOptions()
        {
            this.CheckFileExists = true;
        }

        public bool Multiselect { get; set; }
        public bool ReadOnlyChecked { get; set; }
        public bool ShowReadOnly { get; set; }
    }
}
