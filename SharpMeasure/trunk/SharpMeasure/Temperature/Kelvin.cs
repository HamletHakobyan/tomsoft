namespace SharpMeasure.Temperature
{
    public class Kelvin : IUnit<ITemperature>
    {
        public string Symbol { get { return "K"; } }

        public double ToSIUnit(double value)
        {
            return value;
        }

        public double FromSIUnit(double value)
        {
            return value;
        }
    }
}
