using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Dialogs
{
    public interface IFileDialogService
    {
        bool? ShowOpen(ref string fileName);
        bool? ShowSave(ref string fileName);
    }
}
