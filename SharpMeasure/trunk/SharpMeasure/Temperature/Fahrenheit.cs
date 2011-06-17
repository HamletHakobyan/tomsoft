namespace SharpMeasure.Temperature
{
    public class Fahrenheit : IUnit<ITemperature>
    {
        public string Symbol { get { return "°F"; } }

        public double ToSIUnit(double value)
        {
            return (value + 459.67) / 1.8;
        }

        public double FromSIUnit(double value)
        {
            return value * 1.8 - 459.67;
        }
    }
}
