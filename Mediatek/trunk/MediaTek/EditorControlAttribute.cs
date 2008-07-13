using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTek
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
