using System;
using Mediatek.Entities;
using Mediatek.Service;
using Mediatek.ViewModel.Editors;

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

        protected override IDialogViewModel GetEditor()
        {
            return new BookEditorViewModel(this);
        }
    }
}
