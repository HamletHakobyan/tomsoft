using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using System.Windows.Data;
using System.ComponentModel;
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
