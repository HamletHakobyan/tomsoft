using Mediatek.Entities;

namespace Mediatek.ViewModel
{
    public class BookViewModel : MediaViewModel
    {
        public BookViewModel(Media book)
            : base(book)
        {
        }

        public Book BookModel
        {
            get { return (Book)Model; }
            set { Model = value; }
        }

    }
}
