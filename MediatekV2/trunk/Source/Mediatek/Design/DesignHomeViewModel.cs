using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Design
{
    public class DesignHomeViewModel
    {
        public DesignHomeViewModel()
        {
            DbName = "My media library";
            DbDescription = "My media library";
            MovieCount = 42;
            AlbumCount = 17;
            BookCount = 65;
        }

        public string DbName { get; set; }

        public string DbDescription { get; set; }

        public int MovieCount { get; set; }

        public int AlbumCount { get; set; }

        public int BookCount { get; set; }
    }
}
