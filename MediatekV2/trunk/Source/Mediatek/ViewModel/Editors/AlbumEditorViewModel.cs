using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.ViewModel.Editors
{
    public class AlbumEditorViewModel : EditorViewModelBase
    {
        private readonly AlbumViewModel _album;

        public AlbumEditorViewModel(AlbumViewModel album)
        {
            _album = album;
        }
    }
}
