using System;

namespace SharpMeasure
{
    public struct Measure<TUnit> : IEquatable<Measure<TUnit>>, IComparable<Measure<TUnit>>
        where TUnit : IUnit, new()
    {
        #region Private data

        private static readonly IUnit _unit;
        private readonly double _value;

        #endregion

        #region Constructors

        static Measure()
        {
            _unit = Units.GetUnit<TUnit>();
        }
        
        public Measure(double value)
        {
            _value = value;
        }

        #endregion

        #region Public properties

        public double Value { get { return _value; } }
        public IUnit Unit { get { return _unit; } }

        #endregion

        #region Object method overrides

        public override string ToString()
        {
            return string.Format("{0} {1}", _value, _unit.Symbol);
        }

        public override bool Equals(object obj)
        {
            if (obj is Measure<TUnit>)
                return Equals((Measure<TUnit>)obj);
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region IEquatable<T> implementation

        public bool Equals(Measure<TUnit> other)
        {
            return other.Value == _value;
        }

        #endregion

        #region IComparable<T> implementation

        public int CompareTo(Measure<TUnit> other)
        {
            return _value.CompareTo(other.Value);
        }

        #endregion

        #region Operators

        public static implicit operator Measure<TUnit>(double value)
        {
            return new Measure<TUnit>(value);
        }

        public static explicit operator double(Measure<TUnit> measure)
        {
            return measure.Value;
        }

        #endregion

        #region Conversion to other units

        public Measure<TTargetUnit> Convert<TTargetUnit>()
            where TTargetUnit : IUnit, new()
        {
            if (!Units.AreSameQuantity<TUnit, TTargetUnit>())
                throw new IncompatibleUnitsException(typeof(TUnit), typeof(TTargetUnit));
            IUnit otherUnit = Units.GetUnit<TTargetUnit>();
            double value = otherUnit.FromSIUnit(_unit.ToSIUnit(_value));
            return new Measure<TTargetUnit>(value);
        }

        #endregion

        #region Mathematical operators and methods

        public static Measure<TUnit> operator +(Measure<TUnit> x, Measure<TUnit> y)
        {
            return new Measure<TUnit>(x.Value + y.Value);
        }

        public static Measure<TUnit> operator -(Measure<TUnit> x, Measure<TUnit> y)
        {
            return new Measure<TUnit>(x.Value - y.Value);
        }

        public static Measure<TUnit> operator *(Measure<TUnit> measure, double multiplier)
        {
            return new Measure<TUnit>(measure.Value * multiplier);
        }

        public static Measure<TUnit> operator *(double multiplier, Measure<TUnit> measure)
        {
            return new Measure<TUnit>(measure.Value * multiplier);
        }

        public static Measure<TUnit> operator /(Measure<TUnit> measure, double divisor)
        {
            return new Measure<TUnit>(measure.Value / divisor);
        }

        public Measure<FractionalUnit<TUnit, TDivisorUnit>> DivideBy<TDivisorUnit>(Measure<TDivisorUnit> divisor)
            where TDivisorUnit : IUnit, new()
        {
            return new Measure<FractionalUnit<TUnit, TDivisorUnit>>(_value / divisor.Value);
        }

        public Measure<TDenominator> DivideBy<TDenominator>(Measure<FractionalUnit<TUnit, TDenominator>> divisor)
            where TDenominator : IUnit, new()
        {
            return new Measure<TDenominator>(_value / divisor.Value);
        }

        public Measure<NoUnit> DivideBy(Measure<TUnit> divisor)
        {
            return new Measure<NoUnit>(_value / divisor.Value);
        }

        public Measure<TUnit> DivideBy(Measure<NoUnit> divisor)
        {
            return new Measure<TUnit>(_value / divisor.Value);
        }

        public Measure<ProductUnit<TUnit, TMultiplierUnit>> MultiplyBy<TMultiplierUnit>(Measure<TMultiplierUnit> multiplier)
            where TMultiplierUnit : IUnit, new()
        {
            return new Measure<ProductUnit<TUnit, TMultiplierUnit>>(_value * multiplier.Value);
        }

        public Measure<TNumerator> MultiplyBy<TNumerator>(Measure<FractionalUnit<TNumerator, TUnit>> multiplier)
            where TNumerator : IUnit, new()
        {
            return new Measure<TNumerator>(_value * multiplier.Value);
        }

        public Measure<NoUnit> MultiplyBy(Measure<FractionalUnit<NoUnit, TUnit>> multiplier)
        {
            return new Measure<NoUnit>(_value * multiplier.Value);
        }

        public Measure<TUnit> MultiplyBy(Measure<NoUnit> multiplier)
        {
            return new Measure<TUnit>(_value * multiplier.Value);
        }

        #endregion
    }
}
