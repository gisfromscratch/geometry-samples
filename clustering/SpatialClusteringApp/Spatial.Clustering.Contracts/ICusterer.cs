namespace Spatial.Clustering.Contracts
{
    /// <summary>
    /// Provides clustering methods.
    /// </summary>
    public interface IClusterer
    {
        /// <summary>
        /// Calculates the distance between two elements.
        /// </summary>
        /// <param name="element">An element.</param>
        /// <param name="otherElement">Another element.</param>
        /// <returns>The distance between those two elements.</returns>
        double CalculateDistance(IClusterable element, IClusterable otherElement);
    }
}
