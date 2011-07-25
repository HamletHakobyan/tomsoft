using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Mediatek.Design
{
    public class DesignMediaViewModel
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int? Year { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public byte? Rating { get; set; }
        public DateTime? DateAdded { get; set; }

        public object Language { get; set; }
        public ICollection<object> Countries { get; set; }
        public ICollection<object> Loans { get; set; }
        public ImageSource Picture { get; set; }
        public ICollection<object> Contributions { get; set; }
    }
}
