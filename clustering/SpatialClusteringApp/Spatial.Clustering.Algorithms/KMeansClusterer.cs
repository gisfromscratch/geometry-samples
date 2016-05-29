using System;
using System.Collections.Generic;
using Spatial.Clustering.Contracts;
using System.Linq;
using Spatial.Clustering.Data;

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
            AssignElementsToClusters(elementsAsList, clusters);
            var maxIterationCount = 1E3;
            for (var iteration = 0; iteration < maxIterationCount; iteration++)
            {
                if (UpdateClustersCenter(clusters))
                {
                    break;
                }
                foreach (var cluster in clusters)
                {
                    cluster.RemoveAll();
                }
                AssignElementsToClusters(elementsAsList, clusters);
            }
            return clusters;
        }

        /// <summary>
        /// Creates random clusters.
        /// Each cluster contains no elements and uses one element as the center of the cluster.
        /// </summary>
        /// <param name="elements">The elements of all clusters.</param>
        /// <param name="clusterCount">The number of clusters.</param>
        /// <returns>Newly created random clusters having only a center.</returns>
        private IEnumerable<SimpleCluster> CreateRandomClusters(IList<IClusterable> elements, int clusterCount)
        {
            var clusters = new List<SimpleCluster>(clusterCount);
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
                        cluster.Center = nextElement;
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

        /// <summary>
        /// Updates the center of all clusters using their elements.
        /// If the calculated center is identical to the existing center of a cluster, the center is reused.
        /// </summary>
        /// <param name="clusters">The clusters having elements and a valid center.</param>
        /// <returns><code>true</code> when at least one center was updated.</returns>
        private bool UpdateClustersCenter(IEnumerable<SimpleCluster> clusters)
        {
            var centerWasUpdated = false;
            foreach (var cluster in clusters)
            {
                var existingCenter = cluster.Center;
                var sumOfValues = new double[existingCenter.Dimension];
                var numberOfElements = 0;
                foreach (var element in cluster.Elements)
                {
                    numberOfElements++;
                    for (uint dimension = 0; dimension < element.Dimension; dimension++)
                    {
                        sumOfValues[dimension] += element.GetValue(dimension);
                    }
                }

                if (0 < numberOfElements)
                {
                    var averageOfValues = new double[existingCenter.Dimension];
                    for (uint dimension = 0; dimension < existingCenter.Dimension; dimension++)
                    {
                        averageOfValues[dimension] = sumOfValues[dimension] / numberOfElements;
                    }

                    const double Epsilon = 1E-5;
                    var calculatedCenter = new SimpleClusterElement(averageOfValues);
                    var distanceBetweenCenters = _distanceCalculator.CalculateDistance(existingCenter, calculatedCenter);
                    if (Epsilon < distanceBetweenCenters)
                    {
                        cluster.Center = calculatedCenter;
                        centerWasUpdated = true;
                    }
                }
            }
            return centerWasUpdated;
        }

        /// <summary>
        /// Assigns the elements to the clusters using the nearest center of all clusters.
        /// </summary>
        /// <param name="elements">The elements of all clusters.</param>
        /// <param name="clusters">The clusters having a valid center.</param>
        private void AssignElementsToClusters(IEnumerable<IClusterable> elements, IEnumerable<SimpleCluster> clusters)
        {
            foreach (var element in elements)
            {
                AssignElementToCluster(element, clusters);
            }
        }

        /// <summary>
        /// Assign one element to a cluster using the nearest center of all clusters.
        /// </summary>
        /// <param name="element">The element which should be assigned.</param>
        /// <param name="clusters">The clusters having a valid center.</param>
        private void AssignElementToCluster(IClusterable element, IEnumerable<SimpleCluster> clusters)
        {
            double minimumDistance = double.MaxValue;
            SimpleCluster nearestCluster = null;
            foreach (var cluster in clusters)
            {
                var distance = _distanceCalculator.CalculateDistance(element, cluster.Center);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestCluster = cluster;
                }
            }

            if (null != nearestCluster)
            {
                nearestCluster.Add(element);
            }
            else
            {
                throw new ArgumentException(nameof(clusters));
            }
        }
    }
}
