using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Mediatek.Entities;
using System.Windows.Media;
using Mediatek.Helpers;
using System.Windows.Media.Imaging;

namespace Mediatek.ViewModel
{
    public abstract class MediaViewModel : ViewModelBase<Media>
    {
        protected MediaViewModel(Media media)
        {
            this.Model = media;
        }

        public Guid Id
        {
            get { return Model.Id; }
        }

        private BitmapSource _picture;
        public BitmapSource Picture
        {
            get
            {
                if (_picture == null && Model.PictureId.HasValue)
                {
                    Model.Picture.GetBitmapSourceAsync(
                        bmp =>
                        {
                            _picture = bmp;
                            OnPropertyChanged("Picture");
                        });
                    return null;
                }
                return _picture;
            }
            set
            {
                if (value != _picture)
                {
                    Model.Picture.SetImageData(value);
                    _picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }

    }
}
