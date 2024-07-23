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
        /// Applies the character filter to the input word.
        /// Filters out the word if it contains any of the specified characters.
        /// </summary>
        /// <param name="word">The input word to filter.</param>
        /// <returns>True if the word passes the filter, otherwise false.</returns>
        public bool Apply(string word)
        {
            // Filter word based on the presence of any specified characters
            return !_characters.Any(c => word.Contains(c));
        }
    }
}