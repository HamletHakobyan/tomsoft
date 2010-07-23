using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Model.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbProviderNameAttribute : Attribute
    {
        public DbProviderNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
