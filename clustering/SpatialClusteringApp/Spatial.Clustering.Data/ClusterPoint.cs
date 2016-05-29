using System;
using Spatial.Clustering.Contracts;

namespace Spatial.Clustering.Data
{
    /// <summary>
    /// Represents a two dimensional point.
    /// </summary>
    public class ClusterPoint : IClusterable
    {
        /// <summary>
        /// Creates a new point having x and y coordinates.
        /// </summary>
        public ClusterPoint()
        {
            X = double.NaN;
            Y = double.NaN;
        }

        public bool IsEmpty
        {
            get
            {
                return double.IsNaN(X) || double.IsNaN(Y);
            }
        }

        public void SetEmpty()
        {
            X = double.NaN;
            Y = double.NaN;
        }

        public uint Dimension
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// The x coordinate of this point.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The y coordinate of this point.
        /// </summary>
        public double Y { get; set; }

        public double GetValue(uint dimension)
        {
            switch (dimension)
            {
                case 0:
                    return X;
                case 1:
                    return Y;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dimension));
            }
        }
    }
}
