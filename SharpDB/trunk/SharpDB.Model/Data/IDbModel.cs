using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace SharpDB.Model.Data
{
    public interface IDbModel
    {
        void InitModel(DbConnection connection);
        IDbItemGroup[] ItemGroups { get; }
    }
}
