using System.IO;
using System.Windows.Media.Imaging;
using Mediatek.Entities;

namespace Mediatek.Helpers
{
    static class ImageHelper
    {
        public static BitmapSource GetBitmapSource(this Image image)
        {
            if (image.Data != null)
            {
                return BitmapSourceFromBytes(image.Data.Bytes);
            }
            return null;
        }

        public static BitmapSource BitmapSourceFromBytes(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
        }

        public static void SetImageData(this Image image, BitmapSource bitmapSource)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                image.Data.Bytes = ms.ToArray();
            }
        }

        public static void SetImageData(this Image image, string fileName)
        {
            image.Data.Bytes = File.ReadAllBytes(fileName);
        }
    }
}
