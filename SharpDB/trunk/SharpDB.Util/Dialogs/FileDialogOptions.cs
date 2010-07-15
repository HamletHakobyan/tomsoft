using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SharpDB.Util.Dialogs
{
    public abstract class FileDialogOptions
    {
        public FileDialogOptions()
        {
            this.InitialDirectory = string.Empty;
            this.Title = string.Empty;
            this.Filter = string.Empty;
            this.CheckPathExists = true;
            this.AddExtension = true;
        }

        public string InitialDirectory { get; set; }
        public string Title { get; set; }
        public string Filter { get; set; }
        public int FilterIndex { get; set; }
        public bool CheckFileExists { get; set; }
        public bool CheckPathExists { get; set; }
        public bool AddExtension { get; set; }
        public string DefaultExt { get; set; }
        public bool DereferenceLinks { get; set; }
        public CancelEventHandler FileOk { get; set; }
        public bool RestoreDirectory { get; set; }
        public bool ValidateNames { get; set; }
        public string[] FileNames { get; set; }
    }
}
