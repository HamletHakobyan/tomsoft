namespace SharpMeasure.Length
{
    public class Mile : IUnit<ILength>
    {
        public string Symbol { get { return "mi"; } }

        public double ToSIUnit(double value)
        {
            return value * 1609.344;
        }

        public double FromSIUnit(double value)
        {
            return value / 1609.344;
        }
    }
}
