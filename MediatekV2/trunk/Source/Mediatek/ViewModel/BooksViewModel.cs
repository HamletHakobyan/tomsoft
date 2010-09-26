using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using System.ComponentModel;
using System.Windows.Data;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class BooksViewModel : ViewModelBase
    {
        public BooksViewModel()
        {
            var rep = App.GetService<IViewModelRepository>();
            this.Books = new CollectionView(rep.Medias)
                             {
                                 Filter = o => o is BookViewModel
                             };
        }

        public ICollectionView Books { get; private set; }
    }
}
