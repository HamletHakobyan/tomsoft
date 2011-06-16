namespace SharpMeasure.Time
{
    public class Minute : IUnit<ITime>
    {
        public string Symbol { get { return "min"; } }
        public double ValueInSIUnit { get { return 60.0; } }
    }
}
