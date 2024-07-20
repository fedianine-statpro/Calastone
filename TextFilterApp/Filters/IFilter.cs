namespace TextFilterApp.Filters
{
    /// <summary>
    /// Interface for text filters.
    /// Defines a method for applying filters to a collection of words.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Applies the filter to the given collection of words.
        /// </summary>
        /// <param name="words">The collection of words to filter.</param>
        /// <returns>The filtered collection of words.</returns>
        IEnumerable<string> Apply(IEnumerable<string> words);
    }
}