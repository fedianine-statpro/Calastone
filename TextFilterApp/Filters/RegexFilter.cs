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
        /// Applies the regex filter to the input words.
        /// Filters out words that match the regex pattern.
        /// </summary>
        /// <param name="words">The input words to filter.</param>
        /// <returns>The filtered words.</returns>
        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            // Filter words based on the regex pattern
            return words.Where(word => !_regex.IsMatch(word));
        }
    }
}