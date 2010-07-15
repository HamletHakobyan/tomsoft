using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util
{
    public struct TextSelection
    {
        private readonly int _start;
        private readonly int _length;

        public TextSelection(int start, int length)
        {
            _start = start;
            _length = length;
        }

        public int Start
        {
            get { return _start; }
        }

        public int Length
        {
            get { return _length; }
        }
    }
}
