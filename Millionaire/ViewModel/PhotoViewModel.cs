using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class PhotoViewModel : SlideViewModel
    {
        private readonly Photo _photo;

        public PhotoViewModel(Photo photo, GameViewModel game)
            : base(game)
        {
            this._photo = photo;
            if (!String.IsNullOrEmpty(_photo.Quiz.ContentPath)) 
            {
                if (!String.IsNullOrEmpty(_photo.SoundPath))
                {
                    string fullPath = Path.Combine(_photo.Quiz.ContentPath, _photo.SoundPath);
                    this.Sound = new Uri(fullPath);
                }
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get
            {
                if (_image == null && !String.IsNullOrEmpty(_photo.Path))
                {
                    if (!string.IsNullOrEmpty(_photo.Quiz.ContentPath))
                    {
                        string fullPath = Path.Combine(_photo.Quiz.ContentPath, _photo.Path);
                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.UriSource = new Uri(fullPath);
                        bmp.EndInit();
                        _image = bmp;
                    }
                }
                return _image;
            }
        }
    }
}
