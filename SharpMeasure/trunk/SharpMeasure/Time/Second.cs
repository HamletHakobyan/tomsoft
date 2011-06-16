using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure.Time
{
    public class Second : IUnit
    {
        public string Symbol { get { return "s"; } }
        public double ValueInSIUnit { get { return 1.0; } }
    }
}
