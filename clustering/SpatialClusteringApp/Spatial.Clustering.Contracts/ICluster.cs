﻿using System.Collections.Generic;

namespace Spatial.Clustering.Contracts
{
    /// <summary>
    /// Represents a cluster of elements.
    /// </summary>
    public interface ICluster
    {
        /// <summary>
        /// The elements of this cluster.
        /// </summary>
        IEnumerable<IClusterable> Elements { get; }
    }
}
