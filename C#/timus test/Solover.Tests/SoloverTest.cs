using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solver;

namespace Solover.Tests
{
    [TestClass]
    public class SoloverTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var countRing = 4;
            var valueOne = 2;
            var valueTwo = 2;

            var result = SolveRing.ModRing(countRing, valueOne, valueTwo);

            Assert.AreEqual(0, result);
        }
    }
}
