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

        #region Reduction of composite units

        // Reduction of 'a/a/b' to 'b'
        public static Measure<TDenominator> Reduce<TNumerator, TDenominator>(this Measure<FractionalUnit<TNumerator, FractionalUnit<TNumerator, TDenominator>>> measure)
            where TNumerator : IUnit, new()
            where TDenominator : IUnit, new()
        {
            return new Measure<TDenominator>(measure.Value);
        }

        // Reduction of 'a/b/a' to '1/b'
        public static Measure<FractionalUnit<NoUnit, TDenominator>> Reduce<TNumerator, TDenominator>(this Measure<FractionalUnit<FractionalUnit<TNumerator, TDenominator>, TNumerator>> measure)
            where TNumerator : IUnit, new()
            where TDenominator : IUnit, new()
        {
            return new Measure<FractionalUnit<NoUnit, TDenominator>>(measure.Value);
        }

        // Reduction of 'a/1' to 'a'
        public static Measure<TNumerator> Reduce<TNumerator>(this Measure<FractionalUnit<TNumerator, NoUnit>> measure)
            where TNumerator : IUnit, new()
        {
            return new Measure<TNumerator>(measure.Value);
        }

        // Reduction of 'b * a/b' to 'a'
        public static Measure<TNumerator> Reduce<TNumerator, TDenominator>(this Measure<ProductUnit<TDenominator, FractionalUnit<TNumerator, TDenominator>>> measure)
            where TNumerator : IUnit, new()
            where TDenominator : IUnit, new()
        {
            return new Measure<TNumerator>(measure.Value);
        }

        // Reduction of 'a/b * b' to 'a'
        public static Measure<TNumerator> Reduce<TNumerator, TDenominator>(this Measure<ProductUnit<FractionalUnit<TNumerator, TDenominator>, TDenominator>> measure)
            where TNumerator : IUnit, new()
            where TDenominator : IUnit, new()
        {
            return new Measure<TNumerator>(measure.Value);
        }

        // Reduction of 'a * 1' to 'a'
        public static Measure<TFirst> Reduce<TFirst>(this Measure<ProductUnit<TFirst, NoUnit>> measure)
            where TFirst : IUnit, new()
        {
            return new Measure<TFirst>(measure.Value);
        }

        // Reduction of '1 * a' to 'a'
        public static Measure<TSecond> Reduce<TSecond>(this Measure<ProductUnit<NoUnit, TSecond>> measure)
            where TSecond : IUnit, new()
        {
            return new Measure<TSecond>(measure.Value);
        }
        

        #endregion
    }
}
