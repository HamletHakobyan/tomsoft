using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediaTek
{
    public class ObjectState
    {
        public Dictionary<string, object> Properties { get; private set; }
        public Dictionary<string, object> Fields { get; private set; }

        public ObjectState()
        {
            this.Properties = new Dictionary<string, object>();
            this.Fields = new Dictionary<string, object>();
        }

        public ObjectState(object target)
            : this()
        {
            BackupState(target);
        }

        public void BackupState(object target)
        {
            var properties = from prop in target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             where prop.CanRead
                             && prop.CanRead
                             && prop.GetIndexParameters().Length == 0
                             select prop;
            
            this.Properties.Clear();
            foreach (PropertyInfo prop in properties)
            {
                object value = prop.GetValue(target, null);
                this.Properties.Add(prop.Name, value);
            }

            var fields = from fld in target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)
                         where !fld.IsInitOnly
                         select fld;
            this.Fields.Clear();
            foreach (FieldInfo fld in fields)
            {
                object value = fld.GetValue(target);
                this.Fields.Add(fld.Name, value);
            }
        }

        public void RestoreState(object target)
        {
            Type targetType = target.GetType();
            foreach (string propName in this.Properties.Keys)
            {
                PropertyInfo prop = targetType.GetProperty(propName);
                if (prop == null)
                    throw new ArgumentException("The target object doesn't have a property named " + propName);
                prop.SetValue(target, this.Properties[propName], null);
            }
            foreach (string fldName in this.Fields.Keys)
            {
                FieldInfo fld = targetType.GetField(fldName);
                if (fld == null)
                    throw new ArgumentException("The target object doesn't have a field named " + fldName);
                fld.SetValue(target, this.Fields[fldName]);
            }
        }
    }
}
