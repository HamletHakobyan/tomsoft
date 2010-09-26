using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Mediatek.Entities;

namespace Mediatek.ViewModel
{
    public class BookViewModel : MediaViewModel
    {
        public BookViewModel(Book book)
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
