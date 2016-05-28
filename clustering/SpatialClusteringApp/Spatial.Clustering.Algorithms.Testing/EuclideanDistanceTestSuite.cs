using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spatial.Clustering.Data;

namespace Spatial.Clustering.Algorithms.Testing
{
    /// <summary>
    /// Tests for the euclidean distance calculator.
    /// </summary>
    [TestClass]
    public class EuclideanDistanceTestSuite
    {
        [TestMethod]
        public void TestEuclideanDistanceBetweenElementsHavingIdenticalCoordinates()
        {
            var calculator = new EuclideanDistanceCalculator();
            var element = new ClusterPoint { X = 0, Y = 0 };
            var otherElement = new ClusterPoint { X = element.X, Y = element.Y };
            var distance = calculator.CalculateDistance(element, otherElement);
            Assert.AreEqual(0.0, distance, double.Epsilon, @"The distance must be 0!");
        }
    }
}
