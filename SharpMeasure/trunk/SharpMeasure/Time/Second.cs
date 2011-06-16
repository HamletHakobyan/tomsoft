namespace SharpMeasure.Time
{
    public class Second : IUnit<ITime>
    {
        public string Symbol { get { return "s"; } }
        public double ValueInSIUnit { get { return 1.0; } }
    }
}
