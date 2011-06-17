namespace SharpMeasure
{
// ReSharper disable UnusedTypeParameter
    public abstract class CompositeUnitBase<TFirst, TSecond> : IUnit
        where TFirst : IUnit, new()
        where TSecond : IUnit, new()
    {
        public abstract string Symbol { get; }
        public abstract double ToSIUnit(double value);
        public abstract double FromSIUnit(double value);
    }
// ReSharper restore UnusedTypeParameter
}
