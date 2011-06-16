using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMeasure
{
    public static class Units
    {
        private static readonly IDictionary<Type, IUnit> _unitCache;
        private static readonly IDictionary<Type, Type> _quantityCache;

        static Units()
        {
            _unitCache = new Dictionary<Type, IUnit>();
            _quantityCache = new Dictionary<Type, Type>();
        }

        public static IUnit GetUnit<TUnit>()
            where TUnit : IUnit, new()
        {
            IUnit unit;
            if (!_unitCache.TryGetValue(typeof(TUnit), out unit))
            {
                unit = new TUnit();
                _unitCache[typeof(TUnit)] = unit;
            }
            return unit;
        }

        public static bool AreSameQuantity<TFirst, TSecond>()
            where TFirst : IUnit, new()
            where TSecond : IUnit, new()
        {
            Type firstQuantity = GetQuantityType<TFirst>();
            Type secondQuantity = GetQuantityType<TSecond>();
            return (firstQuantity == secondQuantity);
        }

        private static Type GetQuantityType<TUnit>()
        {
            Type quantityType;
            if (!_quantityCache.TryGetValue(typeof(TUnit), out quantityType))
            {
                Type unitInterface = typeof(TUnit).GetInterface(typeof(IUnit<>).FullName);
                if (unitInterface != null)
                    quantityType = unitInterface.GetGenericArguments().First();
                _quantityCache[typeof(TUnit)] = quantityType;
            }
            return quantityType;
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
