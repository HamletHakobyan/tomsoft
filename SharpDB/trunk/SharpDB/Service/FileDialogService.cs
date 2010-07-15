using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Dialogs;
using Microsoft.Win32;
using System.IO;
using Developpez.Dotnet;

namespace SharpDB.Service
{
    class FileDialogService : DialogServiceBase, IFileDialogService
    {
        public bool? ShowOpen(ref string fileName, OpenFileDialogOptions options)
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = fileName;
            SetOptions(dialog, options);
            bool? result = dialog.ShowDialog(GetTopWindow());
            if (result == true)
            {
                fileName = dialog.FileName;
                GetOptions(dialog, options);
            }
            return result;
        }

        public bool? ShowSave(ref string fileName, SaveFileDialogOptions options)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = fileName;
            SetOptions(dialog, options);
            bool? result = dialog.ShowDialog(GetTopWindow());
            if (result == true)
            {
                fileName = dialog.FileName;
                GetOptions(dialog, options);
            }
            return result;
        }

        private void SetOptions(OpenFileDialog dialog, OpenFileDialogOptions options)
        {
            SetCommonOptions(dialog, options);
            dialog.Multiselect = options.Multiselect;
            dialog.ReadOnlyChecked = options.ReadOnlyChecked;
            dialog.ShowReadOnly = options.ShowReadOnly;
        }

        private void SetOptions(SaveFileDialog dialog, SaveFileDialogOptions options)
        {
            SetCommonOptions(dialog, options);
            dialog.CreatePrompt = options.CreatePrompt;
            dialog.OverwritePrompt = options.OverwritePrompt;
        }

        private void SetCommonOptions(FileDialog dialog, FileDialogOptions options)
        {
            dialog.Filter = options.Filter;
            dialog.InitialDirectory = options.InitialDirectory;
            dialog.FilterIndex = options.FilterIndex;
            dialog.Title = options.Title;
            dialog.CheckFileExists = options.CheckFileExists;
            dialog.CheckPathExists = options.CheckPathExists;
            dialog.AddExtension = options.AddExtension;
            dialog.DefaultExt = options.DefaultExt;
            dialog.DereferenceLinks = options.DereferenceLinks;
            dialog.RestoreDirectory = options.RestoreDirectory;
            dialog.ValidateNames = options.ValidateNames;
            if (options.FileOk != null)
                dialog.FileOk += options.FileOk;
        }

        private void GetOptions(OpenFileDialog dialog, OpenFileDialogOptions options)
        {
            GetCommonOptions(dialog, options);
            options.ReadOnlyChecked = dialog.ReadOnlyChecked;
        }

        private void GetOptions(SaveFileDialog dialog, SaveFileDialogOptions options)
        {
            GetCommonOptions(dialog, options);
        }

        private void GetCommonOptions(FileDialog dialog, FileDialogOptions options)
        {
            if (!dialog.FileName.IsNullOrEmpty())
                options.InitialDirectory = Path.GetDirectoryName(dialog.FileName);
            options.FilterIndex = dialog.FilterIndex;
            options.FileNames = dialog.FileNames;
        }
    }
}
