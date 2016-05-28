using System;
using Spatial.Clustering.Contracts;

namespace Spatial.Clustering.Algorithms
{
    /// <summary>
    /// Represents an euclidean distance calculator.
    /// </summary>
    public class EuclideanDistanceCalculator : IClusterDistanceCalculator
    {
        public double CalculateDistance(IClusterable element, IClusterable otherElement)
        {
            if (null == element)
            {
                throw new ArgumentNullException(nameof(element));
            }
            if (null == otherElement)
            {
                throw new ArgumentNullException(nameof(otherElement));
            }

            if (element.Dimension != otherElement.Dimension)
            {
                throw new ArgumentException(@"The elements must have the same dimension!");
            }

            var squareDistance = 0.0;
            for (uint dimension = 0; dimension < element.Dimension; dimension++)
            {
                squareDistance += (element.GetValue(dimension) - otherElement.GetValue(dimension)) * (element.GetValue(dimension) - otherElement.GetValue(dimension));
            }
            return Math.Sqrt(squareDistance);
        }
    }
}
