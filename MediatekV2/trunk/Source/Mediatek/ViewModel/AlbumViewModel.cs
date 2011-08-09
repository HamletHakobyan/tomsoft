using System;
using Mediatek.Entities;
using Mediatek.Service;
using Mediatek.ViewModel.Editors;

namespace Mediatek.ViewModel
{
    public class AlbumViewModel : MediaViewModel
    {
        public AlbumViewModel(Media album)
            : base(album)
        {
        }

        public Album AlbumModel
        {
            get { return (Album)Model; }
            set { Model = value; }
        }

        protected override IDialogViewModel GetEditor()
        {
            return new AlbumEditorViewModel(this);
        }
    }
}
