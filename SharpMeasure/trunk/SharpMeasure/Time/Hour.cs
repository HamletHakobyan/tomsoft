using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure.Time
{
    public class Hour : IUnit
    {
        public string Symbol { get { return "h"; } }
        public double ValueInSIUnit { get { return 3600.0; } }
    }
}
