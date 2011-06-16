using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMeasure
{
    public static class Units
    {
        private static readonly IDictionary<Type, IUnit> _unitCache;

        static Units()
        {
            _unitCache = new Dictionary<Type, IUnit>();
        }

        public static IUnit GetUnit<TUnit>()
            where TUnit : IUnit, new()
        {
            IUnit unit;
            if (!_unitCache.TryGetValue(typeof(TUnit), out unit))
            {
                unit = new TUnit();
            }
            return unit;
        }

        #region Creation of unit from numbers

        public static Measure<TUnit> Unit<TUnit>(this double value)
            where TUnit : IUnit, new()
        {
            return new Measure<TUnit>(value);
        }

        public static Measure<TUnit> Unit<TUnit>(this float value)
            where TUnit : IUnit, new()
        {
            return new Measure<TUnit>(value);
        }

        public static Measure<TUnit> Unit<TUnit>(this int value)
            where TUnit : IUnit, new()
        {
            return new Measure<TUnit>(value);
        }

        #endregion
    }
}
