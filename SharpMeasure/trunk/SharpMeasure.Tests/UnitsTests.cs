using NUnit.Framework;
using SharpMeasure.Temperature;

namespace SharpMeasure.Tests
{
    [TestFixture]
    class UnitsTests
    {
        [Test]
        public void TestTemperatures()
        {
            var absoluteZero = 0.Unit<Kelvin>();

            var celsius = absoluteZero.Convert<Celsius>();
            Assert.AreEqual(-273.15, celsius.Value, 0.001);

            var fahrenheit = absoluteZero.Convert<Fahrenheit>();
            Assert.AreEqual(-459.67, fahrenheit.Value, 0.001);

            fahrenheit = celsius.Convert<Fahrenheit>();
            Assert.AreEqual(-459.67, fahrenheit.Value, 0.001);

            celsius = fahrenheit.Convert<Celsius>();
            Assert.AreEqual(-273.15, celsius.Value, 0.001);
        }
    }
}
