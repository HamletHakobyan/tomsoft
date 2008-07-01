using System.Windows;
using Microsoft.Win32;

namespace MediaTek
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MediaTekDataContext DataContext { get; set; }

        public static new App Current
        {
            get
            {
                return Application.Current as App;
            }
        }

        public OpenFileDialog OpenImageDialog
        {
            get { return this.Resources["dlgOpenImage"] as OpenFileDialog; }
        }
    }
}
