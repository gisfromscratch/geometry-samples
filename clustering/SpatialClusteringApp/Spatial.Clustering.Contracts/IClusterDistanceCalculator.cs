namespace Spatial.Clustering.Contracts
{
    /// <summary>
    /// Represents a distance calculator between elements of a cluster.
    /// </summary>
    public interface IClusterDistanceCalculator
    {
        /// <summary>
        /// Calculates the distance between two elements.
        /// </summary>
        /// <param name="element">An element of a cluster.</param>
        /// <param name="otherElement">Another element of a cluster.</param>
        /// <returns>The distance between those two cluster elements.</returns>
        double CalculateDistance(IClusterable element, IClusterable otherElement);
    }
}
