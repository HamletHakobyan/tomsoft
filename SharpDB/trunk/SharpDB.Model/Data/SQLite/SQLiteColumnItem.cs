using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace SharpDB.Model.Data.SQLite
{
    class SQLiteColumnItem : IDbColumnItem
    {
        private SQLiteColumnsItemGroup _group;
        private SQLiteDbModel.ColumnsRow _dataRow;
        private IDbItemGroup[] _itemGroups;

        public SQLiteColumnItem(SQLiteColumnsItemGroup group, SQLiteDbModel.ColumnsRow columnRow)
        {
            _group = group;
            _dataRow = columnRow;
            _itemGroups = new IDbItemGroup[0];
        }

        public IDbItemGroup Group
        {
            get { return _group; }
        }

        public string Name
        {
            get { return _dataRow.COLUMN_NAME; }
        }

        public IDbItemGroup[] ItemGroups
        {
            get { return _itemGroups; }
        }


        public DbItemType ItemType
        {
            get { return DbItemType.Column; }
        }

        public string CustomImageKey
        {
            get { return null; }
        }

        public string DataTypeName
        {
            get { return _dataRow.DATA_TYPE; }
        }

        public Type DataType
        {
            get
            {
                var ds = (SQLiteDbModel)_dataRow.Table.DataSet;
                var row = ds.DataTypes.FindByTypeName(this.DataTypeName);
                if (row != null)
                    return Type.GetType(row.DataType);
                return null;
            }
        }

        public int? NumericPrecision
        {
            get
            {
                return _dataRow.IsNUMERIC_PRECISIONNull()
                          ? default(int?)
                          : _dataRow.NUMERIC_PRECISION;
            }
        }

        public int? NumericScale
        {
            get
            {
                return _dataRow.IsNUMERIC_SCALENull()
                            ? default(int?)
                            : _dataRow.NUMERIC_SCALE;
            }
        }

        public bool IsPrimaryKey
        {
            get { return _dataRow.PRIMARY_KEY; }
        }

        public bool IsUnique
        {
            get { return _dataRow.UNIQUE; }
        }

        public bool IsNullable
        {
            get { return _dataRow.IS_NULLABLE; }
        }

        public bool IsAutoIncrement
        {
            get { return _dataRow.AUTOINCREMENT; }
        }

        public object DefaultValue
        {
            get
            {
                if (_dataRow.COLUMN_HASDEFAULT && !_dataRow.IsCOLUMN_DEFAULTNull())
                {
                    string def = _dataRow.COLUMN_DEFAULT;
                    if (string.IsNullOrEmpty(def))
                        return null;
                    return Convert.ChangeType(def, this.DataType, CultureInfo.InvariantCulture);
                }
                return null;
            }
        }


        public string Description
        {
            get
            {
                return _dataRow.IsDESCRIPTIONNull()
                        ? null
                        : _dataRow.DESCRIPTION;
            }
        }
    }
}
