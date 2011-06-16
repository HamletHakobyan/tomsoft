using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure.Time
{
    public class Minute : IUnit
    {
        public string Symbol { get { return "min"; } }
        public double ValueInSIUnit { get { return 60.0; } }
    }
}
