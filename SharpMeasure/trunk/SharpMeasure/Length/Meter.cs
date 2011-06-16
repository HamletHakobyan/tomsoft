namespace SharpMeasure.Length
{
    public class Meter : IUnit<ILength>
    {
        public string Symbol { get { return "m"; } }
        public double ValueInSIUnit { get { return 1.0; } }
    }

    public class Kilometer : IUnit<ILength>
    {
        public string Symbol { get { return "km"; } }
        public double ValueInSIUnit { get { return 1000.0; } }
    }
}
