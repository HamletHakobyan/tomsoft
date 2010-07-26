using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model.Data;

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
                sb.AppendLine(GetFormattedDataType());
                
                if (!_columnItem.IsNullable)
                    sb.AppendLine("NOT NULL");
                
                if (_columnItem.IsPrimaryKey)
                    sb.AppendLine("PRIMARY KEY");
                else if (_columnItem.IsUnique)
                    sb.AppendLine("UNIQUE");

                if (_columnItem.IsAutoIncrement)
                    sb.AppendLine("AUTOINCREMENT");
                return sb.ToString();
            }
        }

        protected string GetFormattedDataType()
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
