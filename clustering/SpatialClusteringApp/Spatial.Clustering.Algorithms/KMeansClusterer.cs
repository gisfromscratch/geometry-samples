using System;
using System.Collections.Generic;
using Spatial.Clustering.Contracts;
using System.Linq;

namespace Spatial.Clustering.Algorithms
{
    /// <summary>
    /// Represents a k-means clusterer.
    /// </summary>
    public class KMeansClusterer : IClusterer
    {
        private readonly IClusterDistanceCalculator _distanceCalculator;
        private readonly Random _random;

        /// <summary>
        /// Creates a new k-means clusterer instance.
        /// </summary>
        /// <param name="distanceCalculator">The distance calculator.</param>
        public KMeansClusterer(IClusterDistanceCalculator distanceCalculator)
        {
            if (null == distanceCalculator)
            {
                throw new ArgumentNullException(nameof(distanceCalculator));
            }

            _distanceCalculator = distanceCalculator;
            _random = new Random();
        }

        public IEnumerable<ICluster> CreateClusters(IEnumerable<IClusterable> elements, uint clusterCount)
        {
            if (int.MaxValue < clusterCount)
            {
                throw new ArgumentOutOfRangeException(nameof(clusterCount));
            }
            if (clusterCount < 1)
            {
                return Enumerable.Empty<ICluster>();
            }
            if (clusterCount == 1)
            {
                var cluster = new SimpleCluster();
                cluster.AddAll(elements);
                return new List<ICluster> { cluster };
            }

            var elementsAsList = elements as IList<IClusterable>;
            if (null == elementsAsList)
            {
                elementsAsList = elements.ToList();
            }
            int clusterCountAsInt = (int)clusterCount;
            if (elementsAsList.Count < clusterCountAsInt)
            {
                return Enumerable.Empty<ICluster>();
            }

            var clusters = CreateRandomClusters(elementsAsList, clusterCountAsInt);
            return clusters;
        }

        private IEnumerable<ICluster> CreateRandomClusters(IList<IClusterable> elements, int clusterCount)
        {
            var clusters = new List<ICluster>(clusterCount);
            var centerIndices = new HashSet<int>();
            var maxElementIndex = elements.Count - 1;
            var maxIterationCount = 1E3;
            for (var clusterIndex = 0; clusterIndex < clusterCount; clusterIndex++)
            {
                var newCenterFound = false;
                for (var iteration = 0; !newCenterFound && iteration < maxIterationCount; iteration++)
                {
                    var nextElementIndex = _random.Next(0, maxElementIndex);
                    if (!centerIndices.Contains(nextElementIndex))
                    {
                        var nextElement = elements[nextElementIndex];
                        var cluster = new SimpleCluster();
                        cluster.Add(nextElement);
                        clusters.Add(cluster);
                        centerIndices.Add(nextElementIndex);
                        newCenterFound = true;
                        break;
                    }
                }
                if (!newCenterFound)
                {
                    throw new ArgumentException(nameof(elements));
                }
            }
            return clusters;
        }
    }
}
