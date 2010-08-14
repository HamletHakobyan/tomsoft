using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    sealed class UnsolvedAttribute : Attribute
    {
        private readonly string _comment;

        public UnsolvedAttribute()
        {
            _comment = string.Empty;
        }

        public UnsolvedAttribute(string comment)
        {
            _comment = comment;
        }

        public string Comment
        {
            get { return _comment; }
        }
    }
}
