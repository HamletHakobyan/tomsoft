using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    public interface IDbColumnItem : IDbModelItem
    {
        string DataTypeName { get; }
        Type DataType { get; }
        int? NumericPrecision { get; }
        int? NumericScale { get; }
        bool IsPrimaryKey { get; }
        bool IsUnique { get; }
        bool IsNullable { get; }
        bool IsAutoIncrement { get; }
        object DefaultValue { get; }
    }
}
