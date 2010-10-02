using Mediatek.Entities;

namespace Mediatek.ViewModel
{
    public class AlbumViewModel : MediaViewModel
    {
        public AlbumViewModel(Media album)
            : base(album)
        {
        }

        public Album AlbumModel
        {
            get { return (Album)Model; }
            set { Model = value; }
        }

    }
}
