namespace SharpMeasure.Length
{
    public class Mile : IUnit<ILength>
    {
        public string Symbol { get { return "mi"; } }
        public double ValueInSIUnit { get { return 1609.344; } }
    }
}
