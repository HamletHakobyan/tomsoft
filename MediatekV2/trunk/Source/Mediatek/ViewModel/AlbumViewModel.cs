using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Mediatek.Entities;

namespace Mediatek.ViewModel
{
    public class AlbumViewModel : MediaViewModel
    {
        public AlbumViewModel(Album album)
            : base(album)
        {
        }

        public Album AlbumModel
        {
            get { return (Album)Model; }
            set { Model = value; }
        }

    }
}
