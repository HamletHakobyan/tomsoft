using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.ViewModel
{
    interface IDatabaseChildItem
    {
        DatabaseViewModel Database { get; }
    }
}
