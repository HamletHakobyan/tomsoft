namespace SharpMeasure
{
    public interface IUnit
    {
        string Symbol { get; }
        double ValueInSIUnit { get; }
    }

// ReSharper disable UnusedTypeParameter
    public interface IUnit<TQuantity> : IUnit
        where TQuantity : IQuantity
    {
    }
// ReSharper restore UnusedTypeParameter
}
