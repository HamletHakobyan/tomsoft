namespace SharpMeasure
{
    public class NoUnit : IUnit<IDimensionless>
    {
        public string Symbol { get { return string.Empty; } }

        public double ToSIUnit(double value)
        {
            return value;
        }

        public double FromSIUnit(double value)
        {
            return value;
        }
    }

    public interface IDimensionless : IQuantity
    {
    }
}
