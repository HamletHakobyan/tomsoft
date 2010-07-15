using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Dialogs
{
    public interface IFileDialogService
    {
        bool? ShowOpen(ref string fileName, OpenFileDialogOptions options);
        bool? ShowSave(ref string fileName, SaveFileDialogOptions options);
    }
}
