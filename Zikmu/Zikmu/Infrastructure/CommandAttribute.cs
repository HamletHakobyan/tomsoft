using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zikmu.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class CommandAttribute : Attribute
    {
        private readonly string _commandName;

        public CommandAttribute()
        {
        }

        public CommandAttribute(string commandName)
        {
            _commandName = commandName;
        }

        public string CommandName
        {
            get { return _commandName; }
        }
    }
}
