namespace SharpMeasure
{
    public interface IUnit
    {
        string Symbol { get; }
        double ToSIUnit(double value);
        double FromSIUnit(double value);
    }

// ReSharper disable UnusedTypeParameter
    public interface IUnit<TQuantity> : IUnit
        where TQuantity : IQuantity
    {
    }
// ReSharper restore UnusedTypeParameter
}
