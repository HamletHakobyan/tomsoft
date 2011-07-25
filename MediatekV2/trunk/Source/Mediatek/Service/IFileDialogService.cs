using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Service
{
    interface IFileDialogService
    {
        bool? ShowOpen(ref string fileName);
        bool? ShowOpen(ref string fileName, FileDialogOptions options);

        bool? ShowSave(ref string fileName);
        bool? ShowSave(ref string fileName, FileDialogOptions options);
    }

    public class FileDialogOptions
    {
        public string Filter { get; set; }
        public int FilterIndex { get; set; }
        public string InitialDirectory { get; set; }
    }
}
