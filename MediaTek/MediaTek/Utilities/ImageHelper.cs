using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;

namespace MediaTek.Utilities
{
    public static class ImageHelper
    {
        public static BitmapSource ImageFromBytes(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = stream;
            img.EndInit();
            return img;
        }

        public static byte[] BytesFromImage(BitmapSource img, BitmapEncoder encoder)
        {
            MemoryStream stream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(img));
            encoder.Save(stream);
            return stream.GetBuffer();
        }

        public static byte[] BytesFromImage(BitmapSource img)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            return BytesFromImage(img, encoder);
        }

        public static BitmapSource FitToSize(BitmapSource img, Size size)
        {
            return FitToSize(img, size.Width, size.Height);
        }

        public static BitmapSource FitToSize(BitmapSource img, double width, double height)
        {
            double xRatio = width / img.Width;
            double yRatio = height / img.Height;
            double ratio = Math.Min(xRatio, yRatio);
            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);
            return Resize(img, newWidth, newHeight);
        }

        public static BitmapSource Resize(BitmapSource source, int width, int height)
        {
            // Target Rect for the resize operation
            Rect rect = new Rect(0, 0, width, height);

            // Create a DrawingVisual/Context to render with
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(source, rect);
            }

            // Use RenderTargetBitmap to resize the original image
            RenderTargetBitmap resizedImage = new RenderTargetBitmap(
                (int)rect.Width, (int)rect.Height,  // Resized dimensions
                96, 96,                             // Default DPI values
                PixelFormats.Default);              // Default pixel format
            resizedImage.Render(drawingVisual);

            // Return the resized image
            return resizedImage;
        }

        public static BitmapSource ImageFromFile(string filename)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(filename);
            img.EndInit();
            return img;
        }

        public static void SaveImage(BitmapSource image, string filename, BitmapEncoder encoder)
        {
            FileStream fs = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write);
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(fs);
            fs.Close();
        }

        public static void SaveImage(BitmapSource image, string filename)
        {
            FileInfo fi = new FileInfo(filename);
            BitmapEncoder encoder = null;
            switch (fi.Extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case ".gif":
                    encoder = new GifBitmapEncoder();
                    break;
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
                case ".tif":
                case ".tiff":
                    encoder = new TiffBitmapEncoder();
                    break;
                case ".wdp":
                    encoder = new WmpBitmapEncoder();
                    break;
                default:
                    throw new NotSupportedException("Unknown file extension, please specify an encoder");
            }
            SaveImage(image, filename, encoder);
        }
    }
}
