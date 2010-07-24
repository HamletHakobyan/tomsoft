using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    public interface IDbModelItem
    {
        IDbItemGroup Group { get; }
        string Name { get; }
        DbItemType ItemType { get; }
        string CustomImageKey { get; }
        IDbItemGroup[] ItemGroups { get; }
    }
}
