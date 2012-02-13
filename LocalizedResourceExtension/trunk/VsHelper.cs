using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace LocalizedResourceExtension
{
    static class VsHelper
    {
        public static T GetProperty<T>(this EnvDTE.Properties properties, string name)
        {
            return GetProperty<T>(properties, name, default(T));
        }

        public static T GetProperty<T>(this EnvDTE.Properties properties, string name, T defaultValue)
        {
            var prop = properties.Cast<Property>().FirstOrDefault(p => p.Name == name);
            if (prop != null)
                return (T) prop.Value;
            return defaultValue;
        }

        public static ProjectItem GetProjectItem(this IVsHierarchy hierarchy, uint itemId)
        {
            object o;
            if (hierarchy.GetProperty(itemId, (int)__VSHPROPID.VSHPROPID_ExtObject, out o) == VSConstants.S_OK)
            {
                return o as ProjectItem;
            }
            return null;
        }
    }
}
