using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure
{
    public interface IUnit
    {
        string Symbol { get; }
        double ValueInSIUnit { get; }
    }
}
