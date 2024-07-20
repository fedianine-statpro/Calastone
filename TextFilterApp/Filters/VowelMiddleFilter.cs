namespace TextFilterApp.Filters
{
    public class VowelMiddleFilter : IFilter
    {
        // Array of vowels to check against
        private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };

        /// <summary>
        /// Applies the vowel middle filter to the input words.
        /// Filters out words that have a vowel in the middle.
        /// </summary>
        /// <param name="words">The input words to filter.</param>
        /// <returns>The filtered words.</returns>
        public IEnumerable<string> Apply(IEnumerable<string> words)
        {
            // Filter words based on the presence of a vowel in the middle
            return words.Where(word =>
            {
                int middleIndex = word.Length / 2;
                if (word.Length % 2 == 0)
                {
                    // For even-length words, check the two middle characters
                    return !Vowels.Contains(char.ToLower(word[middleIndex - 1])) &&
                           !Vowels.Contains(char.ToLower(word[middleIndex]));
                }
                else
                {
                    // For odd-length words, check the single middle character
                    return !Vowels.Contains(char.ToLower(word[middleIndex]));
                }
            });
        }
    }
}