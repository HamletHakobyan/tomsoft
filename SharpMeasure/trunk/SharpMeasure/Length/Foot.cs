using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure.Length
{
    public class Foot : IUnit
    {
        public string Symbol { get { return "ft"; } }
        public double ValueInSIUnit { get { return 0.3048; } }
    }
}
