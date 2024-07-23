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
        /// Applies the length filter to the input word.
        /// Filters out the word if it is shorter than the specified minimum length.
        /// </summary>
        /// <param name="word">The input word to filter.</param>
        /// <returns>True if the word passes the filter, otherwise false.</returns>
        public bool Apply(string word)
        {
            // Filter word based on its length
            return word.Length >= _minLength;
        }
    }
}