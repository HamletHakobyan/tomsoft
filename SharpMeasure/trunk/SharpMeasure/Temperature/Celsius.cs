namespace SharpMeasure.Temperature
{
    public class Celsius : IUnit<ITemperature>
    {
        public string Symbol { get { return "°C"; } }

        public double ToSIUnit(double value)
        {
            return value + 273.15;
        }

        public double FromSIUnit(double value)
        {
            return value - 273.15;
        }
    }
}
