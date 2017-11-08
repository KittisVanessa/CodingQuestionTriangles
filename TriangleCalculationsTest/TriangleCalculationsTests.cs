using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TriangleCalculationsTests
{
    /// <summary>
    /// Basic sanity unit tests. In production would need much more with corner cases and such
    /// </summary>
    [TestClass]
    public class TriangleCalculationsTests
    {
        [TestMethod]
        public void TestToCoordinatesWithEven()
        {
            var result = TriangleCalculations.TriangleCalculator.GetCoordinatesFromName("C6");
            Assert.AreEqual(new Point(30, 40), result[0]);
            Assert.AreEqual(new Point(20, 40), result[1]);
            Assert.AreEqual(new Point(30, 30), result[2]);
            
        }
        [TestMethod]
        public void TestToCoordinatesWithOdd()
        {
            var result = TriangleCalculations.TriangleCalculator.GetCoordinatesFromName("F3");
            Assert.AreEqual(new Point(10,0), result[0]);
            Assert.AreEqual(new Point(20, 0), result[1]);
            Assert.AreEqual(new Point(10,10), result[2]);
        }

        [TestMethod]
        public void TestToNameWithEven()
        {
            var result = TriangleCalculations.TriangleCalculator.GetNameFromCoordinates
                (new Point(30, 40), new Point(20, 40), new Point(30, 30));
            Assert.AreEqual("C6", result);
        }

        [TestMethod]
        public void TestToNameWithOdd()
        {
            var result = TriangleCalculations.TriangleCalculator.GetNameFromCoordinates
                (new Point(10, 0), new Point(10, 10), new Point(20, 0));
            Assert.AreEqual("F3", result);
        }
    }
}
