using System;
using Spatial.Clustering.Contracts;
using System.Collections.Generic;

namespace Spatial.Clustering.Data
{
    /// <summary>
    /// Represents a simple cluster element having values.
    /// </summary>
    public class SimpleClusterElement : IClusterable
    {
        private readonly IList<double> _values;

        /// <summary>
        /// Creates a new instance using a collection of values.
        /// </summary>
        /// <param name="values">The values of this element.</param>
        public SimpleClusterElement(IList<double> values)
        {
            _values = values;
        }

        public uint Dimension
        {
            get
            {
                return (uint) _values.Count;
            }
        }

        public double GetValue(uint dimension)
        {
            if (int.MaxValue < dimension)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            return _values[(int) dimension];
        }
    }
}
