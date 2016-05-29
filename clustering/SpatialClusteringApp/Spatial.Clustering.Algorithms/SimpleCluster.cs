using System.Collections.Generic;
using Spatial.Clustering.Contracts;

namespace Spatial.Clustering.Algorithms
{
    /// <summary>
    /// Represents a simple cluster having elements.
    /// </summary>
    internal class SimpleCluster : ICluster
    {
        private readonly HashSet<IClusterable> _elements;

        /// <summary>
        /// Creates a new cluster instance using a set.
        /// </summary>
        internal SimpleCluster()
        {
            _elements = new HashSet<IClusterable>();
        }

        /// <summary>
        /// Adds an element to this cluster.
        /// </summary>
        /// <param name="element">The element which should be added.</param>
        internal void Add(IClusterable element)
        {
            _elements.Add(element);
        }

        /// <summary>
        /// Adds elements to this cluster.
        /// </summary>
        /// <param name="elements">The elements which should be added.</param>
        internal void AddAll(IEnumerable<IClusterable> elements)
        {
            foreach(var element in elements)
            {
                _elements.Add(element);
            }
        }

        public IEnumerable<IClusterable> Elements
        {
            get { return _elements; }
        }
    }
}
