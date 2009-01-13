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

namespace FlickIt
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

        private string fileName = null;

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
                    fileName = ofd.FileName;
                }
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Save)
            {
                UploadTask task = new UploadTask();
                if (fileName != null)
                {
                    task.Filename = fileName;
                }
                else
                {
                    BitmapSource bmp = image.Source as BitmapSource;
                    MemoryStream ms = new MemoryStream();
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    task.Stream = ms;
                }
                UploadTaskDialog dlg = new UploadTaskDialog();
                dlg.Task = task;
                if (dlg.ShowDialog() == true)
                {
                    App.Current.Tasks.Enqueue(task);
                }
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
                fileName = null;
                e.Handled = true;
            }
        }
    }
}
