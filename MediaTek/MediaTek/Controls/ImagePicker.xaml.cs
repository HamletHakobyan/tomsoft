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
using MediaTek.Utilities;
using System.ComponentModel;

namespace MediaTek.Controls
{
    /// <summary>
    /// Interaction logic for ImagePicker.xaml
    /// </summary>
    public partial class ImagePicker : UserControl
    {
        public ImagePicker()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
                InitializeComponent();
        }

        private static OpenFileDialog dlgOpen = null;
        private static SaveFileDialog dlgSave = null;

        private static void InitOpenFileDialog()
        {
            dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = Properties.Resources.imageFilters;
        }

        private static void InitSaveFileDialog()
        {
            dlgSave = new SaveFileDialog();
            dlgSave.AddExtension = true;
            dlgSave.Filter = Properties.Resources.imageFilters;
        }

        public BitmapSource Image
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(BitmapSource), typeof(ImagePicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (dlgOpen == null)
                InitOpenFileDialog();
            if (dlgOpen.ShowDialog() == true)
            {
                this.Image = ImageHelper.ImageFromFile(dlgOpen.FileName);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.Image == null)
                return;
            if (dlgSave == null)
                InitSaveFileDialog();
            if (dlgSave.ShowDialog() == true)
            {
                ImageHelper.SaveImage(this.Image, dlgSave.FileName);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this image ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Image = null;
            }
        }
    }
}
