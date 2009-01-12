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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Windows.Interop;

namespace ImageShackUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Open)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Save)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Paste)
            {
                e.CanExecute = Clipboard.ContainsImage();
                e.Handled = true;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Open)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tif;*.tiff;*.emf;*.wmf";
                if (ofd.ShowDialog() == true)
                {
                    BitmapSource bmp = new BitmapImage(new Uri(ofd.FileName));
                    image.Source = bmp;
                }
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Save)
            {
                App.Current.SendImage(image.Source as BitmapSource);
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Paste)
            {
                //BitmapSource bmp = Clipboard.GetImage();
                //FormatConvertedBitmap fcb = new FormatConvertedBitmap(bmp, PixelFormats.Bgra32, null, 0);
                //image.Source = fcb;

                System.Drawing.Bitmap bmp = System.Windows.Forms.Clipboard.GetImage() as System.Drawing.Bitmap;
                BitmapSource src = Imaging.CreateBitmapSourceFromHBitmap(
                                        bmp.GetHbitmap(),
                                        IntPtr.Zero,
                                        Int32Rect.Empty,
                                        BitmapSizeOptions.FromEmptyOptions());
                image.Source = src;
                e.Handled = true;
            }
        }
    }
}
