namespace SharpMeasure.Length
{
    public class Foot : IUnit<ILength>
    {
        public string Symbol { get { return "ft"; } }
        public double ValueInSIUnit { get { return 0.3048; } }
    }
}
