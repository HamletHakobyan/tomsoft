using NUnit.Framework;
using SharpMeasure.Length;

namespace SharpMeasure.Tests
{
    [TestFixture]
    class MeasureTests
    {
        [Test]
        public void TestImplicitConversionFromDouble()
        {
            Measure<Meter> distance = 123.0;
            Assert.AreEqual(123.0, distance.Value);
        }

        [Test]
        public void TestUnitExtensionMethod()
        {
            // From int
            var distance = 123.Unit<Meter>();
            Assert.AreEqual(123.0, distance.Value);

            // From double
            distance = 123.0.Unit<Meter>();
            Assert.AreEqual(123.0, distance.Value);

            // From float
            distance = 123f.Unit<Meter>();
            Assert.AreEqual(123.0, distance.Value);
        }

        [Test]
        public void TestDivisionByDimensionlessQuantity()
        {
            var distance = 42.Unit<Meter>().DivideBy(2.Unit<NoUnit>());
            Assert.AreEqual(21, distance.Value);

            distance = 42.Unit<Meter>() / 2;
            Assert.AreEqual(21, distance.Value);
        }
    }
}
