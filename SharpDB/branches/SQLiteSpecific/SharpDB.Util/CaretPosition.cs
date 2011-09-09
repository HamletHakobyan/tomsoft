using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util
{
    public struct CaretPosition
    {
        private readonly int _offset;
        private readonly int _line;

        public CaretPosition(int offset)
            : this(offset, -1)
        {
        }

        public CaretPosition(int offset, int line)
        {
            _offset = offset;
            _line = line;
        }

        public int Offset
        {
            get { return _offset; }
        }

        public int Line
        {
            get { return _line; }
        }

    }
}
