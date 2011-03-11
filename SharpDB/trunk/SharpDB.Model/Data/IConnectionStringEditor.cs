using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    public interface IConnectionStringEditor
    {
        string ConnectionString { get; set; }
        bool? ShowDialog();
        string GetDefaultName();
    }
}
