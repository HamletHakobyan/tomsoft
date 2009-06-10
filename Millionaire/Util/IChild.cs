using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire.Util
{
    public interface IChild<P> where P : class
    {
        P Parent { get; set; }
    }
}
