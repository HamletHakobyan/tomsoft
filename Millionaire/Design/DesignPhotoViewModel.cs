using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Millionaire.Design
{
    public class DesignPhotoViewModel
    {
        public DesignPhotoViewModel()
        {
            var sri = Application.GetResourceStream(new Uri("/Images/BackgroundScore.png", UriKind.Relative));
            if (sri != null)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = sri.Stream;
                bmp.EndInit();
                _image = bmp;
            }
        }

        private readonly ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
        }

    }
}
