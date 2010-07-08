using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Model;

namespace SharpDB.Util.Dialogs
{
    public interface IDataConnectionDialogService
    {
        bool? Show(DatabaseConnection databaseConnection);
    }
}
