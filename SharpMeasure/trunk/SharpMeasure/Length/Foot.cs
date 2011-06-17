namespace SharpMeasure.Length
{
    public class Foot : IUnit<ILength>
    {
        public string Symbol { get { return "ft"; } }

        public double ToSIUnit(double value)
        {
            return value * 0.3048;
        }

        public double FromSIUnit(double value)
        {
            return value / 0.3048;
        }
    }
}
