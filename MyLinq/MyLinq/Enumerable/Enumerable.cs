using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        private static T Identity<T>(T value)
        {
            return value;
        }
    }
}
