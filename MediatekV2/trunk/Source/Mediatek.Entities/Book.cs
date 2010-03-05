using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Book : Media
    {
        public virtual string ISBN { get; set; }
    }
}
