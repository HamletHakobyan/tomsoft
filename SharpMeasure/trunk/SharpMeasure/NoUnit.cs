using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure
{
    public class NoUnit : IUnit<IDimensionless>
    {
        public string Symbol { get { return string.Empty; } }

        public double ValueInSIUnit
        {
            get { return 1.0; }
        }
    }

    public interface IDimensionless : IQuantity
    {
    }
}
