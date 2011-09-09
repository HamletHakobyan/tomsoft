using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace SharpDB.Model.Data
{
    public class DbExceptionWrapper : DbException
    {
        public DbExceptionWrapper(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
    }
}
