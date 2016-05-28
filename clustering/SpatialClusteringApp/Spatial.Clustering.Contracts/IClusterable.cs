using System.Collections.Generic;

namespace Spatial.Clustering.Contracts
{
    /// <summary>
    /// Represents an element which can be used for creating a cluster.
    /// </summary>
    public interface IClusterable
    {
        /// <summary>
        /// The values which should be used for calculating the distance.
        /// </summary>
        IEnumerable<double> Values { get; }
    }
}
