using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MediaTek
{
    /// <summary>
    /// Interaction logic for MovieEditor.xaml
    /// </summary>
    public partial class MovieEditor : UserControl
    {
        public MovieEditor()
        {
            InitializeComponent();
        }

        public Movie Movie
        {
            get { return this.DataContext as Movie; }
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = App.Current.OpenImageDialog;
            if (dlg.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(dlg.FileName);
                img.EndInit();
                Movie.Cover = img;
            }
        }

        private void cmbDirector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Movie.Director = App.Current.DataContext.Directors.Where(d => d.Id == Movie.DirectorId).FirstOrDefault();
        }

        private void cmbLanguage_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Movie.Language = App.Current.DataContext.Languages.Where(l => l.Id == Movie.LanguageId).FirstOrDefault();
        }

        private void cmbMediaType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Movie.MediaType = App.Current.DataContext.MediaTypes.Where(m => m.Id == Movie.MediaTypeId).FirstOrDefault();
        }
    }
}
