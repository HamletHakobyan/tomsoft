using System.Collections.Generic;

namespace MediaTek.Utilities
{
    public static class AttachedValuesExtensions
    {
        private static Dictionary<string, Dictionary<object, object>> attachedProperties = new Dictionary<string, Dictionary<object, object>>();
        public static void SetAttachedValue(this object target, string propertyName, object value)
        {
            if (!attachedProperties.ContainsKey(propertyName))
                attachedProperties.Add(propertyName, new Dictionary<object, object>());
            attachedProperties[propertyName][target] = value;
        }

        public static object GetAttachedValue(this object target, string propertyName)
        {
            if (!attachedProperties.ContainsKey(propertyName))
                return null;
            if (!attachedProperties[propertyName].ContainsKey(target))
                return null;
            return attachedProperties[propertyName][target];
        }

        public static T GetAttachedValue<T>(this object target, string propertyName)
        {
            object value = GetAttachedValue(target, propertyName);
            if (value != null)
                return (T)value;
            else
                return default(T);
        }
    }
}
