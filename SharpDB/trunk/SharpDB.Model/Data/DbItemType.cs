using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    public enum DbItemType
    {
        Table,
        Index,
        Column,
        Custom = -1
    }
}
