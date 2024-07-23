namespace TextFilterApp.Filters
{
    public class VowelMiddleFilter : IFilter
    {
        // Array of vowels to check against
        private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };

        /// <summary>
        /// Applies the vowel middle filter to the input word.
        /// Filters out the word if it has a vowel in the middle.
        /// </summary>
        /// <param name="word">The input word to filter.</param>
        /// <returns>True if the word passes the filter, otherwise false.</returns>
        public bool Apply(string word)
        {
            // Filter word based on the presence of a vowel in the middle
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
        }
    }
}