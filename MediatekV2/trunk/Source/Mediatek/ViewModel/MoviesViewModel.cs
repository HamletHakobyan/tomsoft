using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class MoviesViewModel : MediatekViewModelBase
    {
        public MoviesViewModel()
        {
            var rep = GetService<IViewModelRepository>();
            this.Movies = new CollectionView(rep.Medias)
                              {
                                  Filter = o => o is MovieViewModel
                              };
        }

        public ICollectionView Movies { get; private set; }
    }
}
