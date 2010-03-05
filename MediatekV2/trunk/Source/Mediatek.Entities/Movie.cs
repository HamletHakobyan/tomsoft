using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Movie : Media
    {
        public virtual int? Duration { get; set; }
    }
}
