using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTek
{
    public class PatternFilter
    {
        public string Pattern { get; set; }

        public PatternFilter()
            : this("")
        {
        }

        public PatternFilter(string pattern)
        {
            this.Pattern = pattern;
            Predicate = IsMatch;
        }

        protected bool IsMatch(object obj)
        {
            if (obj is IFilterable)
            {
                return (obj as IFilterable).IsMatch(this.Pattern);
            }
            else
            {
                throw new ArgumentException("The object o filter must implement IFilterable");
            }
        }

        public Predicate<object> Predicate { get; private set; }
    }
}
