using System.Text.RegularExpressions;

namespace TextFilterApp.Filters
{
    /// <summary>
    /// Class responsible for filtering words based on a specified regex pattern.
    /// </summary>
    public class RegexFilter : IFilter
    {
        // Compiled regex instance for matching words
        private readonly Regex _regex;

        /// <summary>
        /// Initializes a new instance of the RegexFilter class with the specified regex pattern.
        /// </summary>
        /// <param name="pattern">The regex pattern to filter words.</param>
        public RegexFilter(string pattern)
        {
            _regex = new Regex(pattern, RegexOptions.Compiled);
        }

        /// <summary>
        /// Applies the regex filter to the input word.
        /// Filters out the word if it matches the regex pattern.
        /// </summary>
        /// <param name="word">The input word to filter.</param>
        /// <returns>True if the word passes the filter, otherwise false.</returns>
        public bool Apply(string word)
        {
            // Filter word based on the regex pattern
            return !_regex.IsMatch(word);
        }
    }
}