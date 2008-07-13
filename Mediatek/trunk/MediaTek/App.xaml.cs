using System.Windows;
using Microsoft.Win32;
using System.Windows.Data;
using System.ComponentModel;

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

        public ICollectionView Movies { get; private set; }
        public ICollectionView Directors { get; private set; }
        public ICollectionView Countries { get; private set; }
        public ICollectionView Languages { get; private set; }
        public ICollectionView MediaTypes { get; private set; }
        public ICollectionView Lends { get; private set; }

        public void InitializeViews()
        {
            this.Movies = new CollectionView(DataContext.Movies);
            this.Directors = new CollectionView(DataContext.Directors);
            this.Countries = new CollectionView(DataContext.Countries);
            this.Languages = new CollectionView(DataContext.Languages);
            this.MediaTypes = new CollectionView(DataContext.MediaTypes);
            this.Lends = new CollectionView(DataContext.Lends);
        }
    }
}
