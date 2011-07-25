using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Mediatek.Service.Implementation
{
    class FileDialogService : IFileDialogService
    {
        public bool? ShowOpen(ref string fileName)
        {
            return ShowOpen(ref fileName, null);
        }

        public bool? ShowOpen(ref string fileName, FileDialogOptions options)
        {
            var dialog = new OpenFileDialog();
            if (options != null)
            {
                dialog.Filter = options.Filter;
                dialog.FilterIndex = options.FilterIndex;
                dialog.InitialDirectory = options.InitialDirectory;
            }
            dialog.FileName = fileName;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                fileName = dialog.FileName;
            }
            return result;
        }

        public bool? ShowSave(ref string fileName)
        {
            return ShowSave(ref fileName, null);
        }

        public bool? ShowSave(ref string fileName, FileDialogOptions options)
        {
            var dialog = new SaveFileDialog();
            if (options != null)
            {
                dialog.Filter = options.Filter;
                dialog.FilterIndex = options.FilterIndex;
                dialog.InitialDirectory = options.InitialDirectory;
            }
            dialog.FileName = fileName;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                fileName = dialog.FileName;
            }
            return result;
        }
    }
}
