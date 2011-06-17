namespace SharpMeasure.Length
{
    public class Meter : IUnit<ILength>
    {
        public string Symbol { get { return "m"; } }
        
        public double ToSIUnit(double value)
        {
            return value;
        }

        public double FromSIUnit(double value)
        {
            return value;
        }
    }

    public class Kilometer : IUnit<ILength>
    {
        public string Symbol { get { return "km"; } }
        
        public double ToSIUnit(double value)
        {
            return value * 1000.0;
        }

        public double FromSIUnit(double value)
        {
            return value / 1000.0;
        }
    }
}
