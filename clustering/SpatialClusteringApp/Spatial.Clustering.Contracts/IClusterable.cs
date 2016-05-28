namespace Spatial.Clustering.Contracts
{
    /// <summary>
    /// Represents an element which can be used for creating a cluster.
    /// </summary>
    public interface IClusterable
    {
        /// <summary>
        /// The dimension (number of values) of this element.
        /// </summary>
        uint Dimension { get; }

        /// <summary>
        /// Gets the value in the specified dimension (index).
        /// </summary>
        /// <param name="dimension">The dimension of the value starting at <code>0</code>.</param>
        /// <returns>The value representing the element.</returns>
        double GetValue(uint dimension);
    }
}
