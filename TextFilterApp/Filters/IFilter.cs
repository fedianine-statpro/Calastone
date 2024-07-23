namespace TextFilterApp.Filters
{
    /// <summary>
    /// Interface for text filters.
    /// Defines a method for applying filters to a single word.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Applies the filter to the given word.
        /// </summary>
        /// <param name="word">The word to filter.</param>
        /// <returns>True if the word passes the filter, otherwise false.</returns>
        bool Apply(string word);
    }
}