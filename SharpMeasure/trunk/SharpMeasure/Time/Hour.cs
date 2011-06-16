namespace SharpMeasure.Time
{
    public class Hour : IUnit<ITime>
    {
        public string Symbol { get { return "h"; } }
        public double ValueInSIUnit { get { return 3600.0; } }
    }
}
