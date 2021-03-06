﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spatial.Clustering.Data;
using System.Collections.Generic;
using System.IO;

namespace Spatial.Clustering.Algorithms.Testing
{
    /// <summary>
    /// Tests for the k-means cluster algorithm.
    /// </summary>
    [TestClass]
    public class KMeansClustererTestSuite
    {
        [TestMethod]
        public void TestKMeansClusterAlgorithmWithThreeElements()
        {
            var clusterer = new KMeansClusterer(new EuclideanDistanceCalculator());
            var elements = new List<ClusterPoint>();
            elements.Add(new ClusterPoint { X = -1, Y = -1 });
            elements.Add(new ClusterPoint { X = 0, Y = 0 });
            elements.Add(new ClusterPoint { X = 1, Y = 1 });
            const uint expectedClusterCount = 2;
            var clusters = clusterer.CreateClusters(elements, expectedClusterCount);
            Assert.IsNotNull(clusters, @"The clusters must not be null!");

            uint clusterCount = 0;
            int expectedElementCount = elements.Count;
            int elementCount = 0;
            foreach (var cluster in clusters)
            {
                clusterCount++;
                foreach (var element in cluster.Elements)
                {
                    elementCount++;
                }
            }
            Assert.AreEqual(expectedClusterCount, clusterCount, @"The cluster count was not expected!");
            Assert.AreEqual(expectedElementCount, elementCount, @"The element count was not expected!");
        }

        [TestMethod]
        public void TestKMeansClusterAlgorithmWithGeonamesCountryFile()
        {
            var clusterer = new KMeansClusterer(new EuclideanDistanceCalculator());
            var elements = new ClusterPointTextFile(new FileInfo(@"data\aa.txt"), "\t", 4, 3, 1);
            const uint expectedClusterCount = 10;
            var clusters = clusterer.CreateClusters(elements, expectedClusterCount);
            Assert.IsNotNull(clusters, @"The clusters must not be null!");

            uint clusterCount = 0;
            foreach (var cluster in clusters)
            {
                clusterCount++;
            }
            Assert.AreEqual(expectedClusterCount, clusterCount, @"The cluster count was not expected!");
        }
    }
}
