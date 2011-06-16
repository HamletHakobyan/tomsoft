namespace SharpMeasure
{
// ReSharper disable UnusedTypeParameter
    public abstract class CompositeUnitBase<TFirst, TSecond> : IUnit
        where TFirst : IUnit, new()
        where TSecond : IUnit, new()
    {
        public abstract string Symbol { get; }
        public abstract double ValueInSIUnit { get; }
    }
// ReSharper restore UnusedTypeParameter
}
