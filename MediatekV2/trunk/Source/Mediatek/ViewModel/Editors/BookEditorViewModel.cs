using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.ViewModel.Editors
{
    public class BookEditorViewModel : EditorViewModelBase
    {
        private readonly BookViewModel _book;

        public BookEditorViewModel(BookViewModel book)
        {
            _book = book;
        }
    }
}
