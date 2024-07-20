namespace TextFilterApp.Filters
{
    /// <summary>
    /// Class responsible for filtering words that contain specified characters.
    /// </summary>
    public class CharacterFilter : IFilter
    {
        // Set of characters to filter out words that contain them
        private readonly HashSet<char> _characters;

        /// <summary>
        /// Initializes a new instance of the CharacterFilter class with the specified characters.
        /// </summary>
        /// <param name="characters">The characters to filter out.</param>
        public CharacterFilter(string characters)
        {
            _characters = new HashSet<char>(characters);
        }

        /// <summary>
        /// Applies the character filter to the input words.
        /// Filters out words that contain any of the specified characters.
        /// </summary>
        /// <param name="words">The input words to filter.</param>
        /// <returns>The filtered words.</returns>
        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            // Filter words based on the presence of any specified characters
            return words.Where(word => !_characters.Any(c => word.Contains(c)));
        }
    }
}