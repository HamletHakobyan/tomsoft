namespace SharpMeasure.Time
{
    public class Minute : IUnit<ITime>
    {
        public string Symbol { get { return "min"; } }
        public double ToSIUnit(double value)
        {
            return value * 60.0;
        }

        public double FromSIUnit(double value)
        {
            return value / 60.0;
        }
    }
}
