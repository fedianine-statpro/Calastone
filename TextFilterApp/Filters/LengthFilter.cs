namespace TextFilterApp.Filters
{
    /// <summary>
    /// Class responsible for filtering words based on their length.
    /// </summary>
    public class LengthFilter : IFilter
    {
        // Minimum length for words to be retained
        private readonly int _minLength;

        /// <summary>
        /// Initializes a new instance of the LengthFilter class with the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum length for words to be retained.</param>
        public LengthFilter(int minLength)
        {
            _minLength = minLength;
        }

        /// <summary>
        /// Applies the length filter to the input words.
        /// Filters out words that are shorter than the specified minimum length.
        /// </summary>
        /// <param name="words">The input words to filter.</param>
        /// <returns>The filtered words.</returns>
        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            // Filter words based on their length
            return words.Where(word => word.Length >= _minLength);
        }
    }
}