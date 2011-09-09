using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    public interface IDbItemGroup
    {
        string Name { get; }
        IDbModelItem[] Items { get; }
        void Refresh();
    }
}
