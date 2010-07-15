using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Service
{
    public class SaveFileDialogOptions : FileDialogOptions
    {
        public SaveFileDialogOptions()
        {
            OverwritePrompt = true;
        }

        public bool CreatePrompt { get; set; }
        public bool OverwritePrompt { get; set; }
    }
}
