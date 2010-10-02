using System;
using System.Linq;
using Mediatek.Entities;
using Mediatek.Helpers;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Threading;

namespace Mediatek.ViewModel
{
    public abstract class MediaViewModel : MediatekViewModelBase<Media>
    {
        protected MediaViewModel(Media media)
        {
            this.Model = media;
        }

        public Guid Id
        {
            get { return Model.Id; }
        }

        public DateTime? DateAdded
        {
            get { return Model.DateAdded; }
            set
            {
                Model.DateAdded = value;
                OnPropertyChanged("DateAdded");
            }
        }

        private BitmapSource _picture;
        public BitmapSource Picture
        {
            get
            {
                if (_picture == null && Model.PictureId.HasValue)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        () =>
                            {
                                _picture = Model.Picture.GetBitmapSource();
                                OnPropertyChanged("Picture");
                            },
                        DispatcherPriority.Background);
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

        public string Title
        {
            get { return Model.Title; }
            set
            {
                if (value != Model.Title)
                {
                    Model.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }


    }
}
