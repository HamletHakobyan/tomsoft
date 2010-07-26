using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;
using System.Globalization;

namespace SharpDB.ViewModel.DbModel
{
    public class DbModelColumnViewModel : DbModelItemViewModel
    {
        private IDbColumnItem _columnItem;

        public DbModelColumnViewModel(DatabaseViewModel database, IDbColumnItem item)
            : base(database, item)
        {
            _columnItem = item;
        }

        public bool IsPrimaryKey
        {
            get { return _columnItem.IsPrimaryKey; }
        }

        public override string SummaryText
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(base.SummaryText);
                sb.AppendFormat(GetResource<string>("column_datatype_format"), GetFormattedDataType()).AppendLine();
                
                if (!_columnItem.IsNullable)
                    sb.AppendLine("NOT NULL");
                
                if (_columnItem.IsPrimaryKey)
                    sb.AppendLine("PRIMARY KEY");
                else if (_columnItem.IsUnique)
                    sb.AppendLine("UNIQUE");

                if (_columnItem.IsAutoIncrement)
                    sb.AppendLine("AUTOINCREMENT");

                if (_columnItem.DefaultValue != null)
                    sb.AppendFormat(GetResource<string>("column_default_value_format"), GetFormattedDefaultValue());

                return sb.ToString();
            }
        }

        protected string GetFormattedDefaultValue()
        {
            Type dataType = _columnItem.DataType;
            object defaultValue = _columnItem.DefaultValue;

            return GetFormattedValue(dataType, defaultValue);
        }

        protected virtual string GetFormattedValue(Type dataType, object defaultValue)
        {
            if (defaultValue == null)
                return "NULL";

            if (dataType == typeof(string))
            {
                return string.Format("'{0}'", ((string)defaultValue).Replace("'", "''"));
            }
            else if (dataType == typeof(byte[]))
            {
                return "BLOB";
            }
            else
            {
                return Convert.ToString(defaultValue, CultureInfo.InvariantCulture);
            }
        }

        protected virtual string GetFormattedDataType()
        {
            string dataTypeName = _columnItem.DataTypeName;
            int? precision = _columnItem.NumericPrecision;
            int? scale = _columnItem.NumericScale;
            if (scale.HasValue)
                return string.Format("{0} ({1}, {2})", dataTypeName, precision.Value, scale.Value);
            if (precision.HasValue)
                return string.Format("{0} ({1})", dataTypeName, precision.Value);
            return dataTypeName;
        }

        protected override System.Windows.Media.ImageSource GetImageInternal()
        {
            if (IsPrimaryKey)
                return GetImageByName("primary_key.png");
            return GetImageByName("column.png");
        }

    }
}
