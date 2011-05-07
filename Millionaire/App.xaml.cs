using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace Millionaire
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current
        {
            get
            {
                return Application.Current as App;
            }
        }

        private string _exePath;
        public string ExePath
        {
            get
            {
                if (_exePath == null)
                {
                    FileInfo file = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
                    _exePath = file.DirectoryName;
                }
                return _exePath;
            }
        }

        public Uri GetSoundPath(string filename)
        {
            string soundDir = Path.Combine(ExePath, "Sounds");
            string soundFile = Path.Combine(soundDir, filename);
            return new Uri(soundFile);
        }
    }
}
