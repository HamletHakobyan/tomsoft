namespace SharpMeasure.Time
{
    public class Hour : IUnit<ITime>
    {
        public string Symbol { get { return "h"; } }
        
        public double ToSIUnit(double value)
        {
            return value * 3600.0;
        }

        public double FromSIUnit(double value)
        {
            return value / 3600.0;
        }
    }
}
