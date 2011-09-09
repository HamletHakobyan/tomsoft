using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    public interface IDbIndexItem : IDbModelItem
    {
        bool IsPrimaryKey { get; }
        bool IsUnique { get; }
    }
}
