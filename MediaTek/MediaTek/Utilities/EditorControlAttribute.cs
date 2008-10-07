using System;

namespace MediaTek.Utilities
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple=false, Inherited=true)]
    public class EditorControlAttribute : Attribute
    {
        public EditorControlAttribute(Type EditorType)
        {
            this.EditorType = EditorType;
        }

        public Type EditorType { get; set; }
    }
}
