using System.Collections.Generic;

namespace Spatial.Clustering.Contracts
{
    /// <summary>
    /// Provides a clustering method.
    /// </summary>
    public interface IClusterer
    {
        /// <summary>
        /// Creates clusters of elements.
        /// </summary>
        /// <param name="elements">The elements to cluster.</param>
        /// <param name="elementCount">The number of clusters.</param>
        /// <returns>Clusters of elements.</returns>
        IEnumerable<ICluster> CreateClusters(IEnumerable<IClusterable> elements, uint clusterCount);
    }
}
