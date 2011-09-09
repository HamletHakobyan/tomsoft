using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class DbProviderSpecificTypeAttribute : Attribute
    {
        public DbProviderSpecificTypeAttribute(string providerName, Type type)
        {
            ProviderName = providerName;
            Type = type;
        }

        public string ProviderName { get; private set; }
        public Type Type { get; set; }
    }
}
