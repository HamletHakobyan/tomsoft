using System.ComponentModel;
using System.Windows.Data;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class BooksViewModel : MediatekViewModelBase
    {
        public BooksViewModel()
        {
            var rep = GetService<IViewModelRepository>();
            this.Books = new CollectionView(rep.Medias)
                             {
                                 Filter = o => o is BookViewModel
                             };
        }

        public ICollectionView Books { get; private set; }
    }
}
