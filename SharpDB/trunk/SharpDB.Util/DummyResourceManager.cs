using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace SharpDB.Util
{
    public class DummyResourceManager : ResourceManager
    {
        public override object GetObject(string name)
        {
            return null;
        }

        public override object GetObject(string name, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override string GetString(string name)
        {
            return name;
        }

        public override string GetString(string name, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override string BaseName
        {
            get
            {
                return "DummyResources";
            }
        }
    }
}
