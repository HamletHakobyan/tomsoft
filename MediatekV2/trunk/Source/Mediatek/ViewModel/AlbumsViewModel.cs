using System.ComponentModel;
using System.Windows.Data;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class AlbumsViewModel : MediatekViewModelBase
    {
        public AlbumsViewModel()
        {
            var rep = GetService<IViewModelRepository>();
            this.Albums = new CollectionView(rep.Medias)
                              {
                                  Filter = o => o is AlbumViewModel
                              };
        }

        public ICollectionView Albums { get; private set; }
    }
}
