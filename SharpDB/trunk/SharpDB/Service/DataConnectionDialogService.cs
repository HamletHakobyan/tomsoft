using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Dialogs;
using SharpDB.Model;
using Microsoft.Data.ConnectionUI;
using Developpez.Dotnet;

namespace SharpDB.Service
{
    class DataConnectionDialogService : IDataConnectionDialogService
    {
        public bool? Show(DatabaseConnection databaseConnection)
        {
            databaseConnection.CheckArgumentNull("databaseConnection");
            var dcd = new DataConnectionDialog();
            var dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(dcd);
            if (!databaseConnection.ProviderName.IsNullOrEmpty() &&
                !databaseConnection.ConnectionString.IsNullOrEmpty())
            {
                var provider = dcd.UnspecifiedDataSource.Providers.FirstOrDefault(p => p.Name == databaseConnection.ProviderName);
                dcd.ConnectionString = databaseConnection.ConnectionString;
            }

            if (DataConnectionDialog.Show(dcd) == System.Windows.Forms.DialogResult.OK)
            {
                databaseConnection.ProviderName = dcd.SelectedDataProvider.Name;
                databaseConnection.ConnectionString = dcd.ConnectionString;
                return true;
            }
            return false;
        }
    }
}
