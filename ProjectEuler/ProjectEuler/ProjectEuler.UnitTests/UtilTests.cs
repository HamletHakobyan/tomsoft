using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectEuler.UnitTests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void TestGCD()
        {
            Assert.AreEqual(3, Util.GCD(6, 15));
            Assert.AreEqual(3, Util.GCD(15, 6));
            Assert.AreEqual(6, Util.GCD(84, 18));
            Assert.AreEqual(6, Util.GCD(18, 84));
            Assert.AreEqual(5, Util.GCD(15, 25));
            Assert.AreEqual(5, Util.GCD(25, 15));
        }

        [TestMethod]
        public void TestLCM()
        {
            Assert.AreEqual(42, Util.LCM(21, 6));
        }

        [TestMethod]
        public void TestLCM_MultipleArgs()
        {
            Assert.AreEqual(504, Util.LCM(8, 9, 21));
        }

        [TestMethod]
        public void TestIsTerminatingDecimal()
        {
            Assert.IsTrue(Util.IsTerminatingDecimal(1, 2));
            Assert.IsTrue(Util.IsTerminatingDecimal(2, 4));
            Assert.IsTrue(Util.IsTerminatingDecimal(3, 6));
            
            Assert.IsTrue(Util.IsTerminatingDecimal(1, 4));
            Assert.IsTrue(Util.IsTerminatingDecimal(2, 8));
            Assert.IsTrue(Util.IsTerminatingDecimal(3, 12));

            Assert.IsTrue(Util.IsTerminatingDecimal(3, 4));
            Assert.IsTrue(Util.IsTerminatingDecimal(6, 8));
            Assert.IsTrue(Util.IsTerminatingDecimal(9, 12));

            Assert.IsTrue(Util.IsTerminatingDecimal(1, 5));
            Assert.IsTrue(Util.IsTerminatingDecimal(2, 10));
            Assert.IsTrue(Util.IsTerminatingDecimal(3, 15));

            Assert.IsTrue(Util.IsTerminatingDecimal(2, 5));
            Assert.IsTrue(Util.IsTerminatingDecimal(4, 10));
            Assert.IsTrue(Util.IsTerminatingDecimal(6, 15));

            Assert.IsFalse(Util.IsTerminatingDecimal(1, 3));
            Assert.IsFalse(Util.IsTerminatingDecimal(2, 6));
            Assert.IsFalse(Util.IsTerminatingDecimal(3, 9));

            Assert.IsFalse(Util.IsTerminatingDecimal(2, 3));
            Assert.IsFalse(Util.IsTerminatingDecimal(4, 6));
            Assert.IsFalse(Util.IsTerminatingDecimal(6, 9));

            Assert.IsFalse(Util.IsTerminatingDecimal(4, 3));
            Assert.IsFalse(Util.IsTerminatingDecimal(12, 7));
            Assert.IsFalse(Util.IsTerminatingDecimal(12, 14));
        }

        [TestMethod]
        public void TestGetDecimalCycleLength()
        {
            Assert.AreEqual(0, Util.GetDecimalCycleLength(1, 2));
            Assert.AreEqual(1, Util.GetDecimalCycleLength(1, 3));
            Assert.AreEqual(0, Util.GetDecimalCycleLength(1, 4));
            Assert.AreEqual(0, Util.GetDecimalCycleLength(1, 5));
            Assert.AreEqual(1, Util.GetDecimalCycleLength(1, 6));
            Assert.AreEqual(6, Util.GetDecimalCycleLength(1, 7));
            Assert.AreEqual(0, Util.GetDecimalCycleLength(1, 8));
            Assert.AreEqual(1, Util.GetDecimalCycleLength(1, 9));
            Assert.AreEqual(0, Util.GetDecimalCycleLength(1, 10));
            Assert.AreEqual(16, Util.GetDecimalCycleLength(1, 17));
            Assert.AreEqual(18, Util.GetDecimalCycleLength(1, 19));
            Assert.AreEqual(22, Util.GetDecimalCycleLength(1, 23));
            Assert.AreEqual(28, Util.GetDecimalCycleLength(1, 29));
            Assert.AreEqual(96, Util.GetDecimalCycleLength(1, 97));
        }
    }
}
