using System.Text.RegularExpressions;
using TextFilterApp.Filters;


namespace TextFilterApp
{

    public class TextProcessor
    {
        private readonly List<IFilter> _filters;

        public TextProcessor(List<IFilter> filters)
        {
            _filters = filters;
        }

        /// <summary>
        /// Applies all the filters to the input text and returns the filtered text.
        /// </summary>
        public string ApplyFilters(string input)
        {
            // Define a regular expression to match words
            var wordRegex = new Regex(@"\b\w+\b", RegexOptions.Compiled);

            // Process each word in the input text
            var result = wordRegex.Replace(input, match =>
            {
                var word = match.Value;
                return _filters.Any(filter => !filter.Apply(word)) ? string.Empty : word;
            });

            return result;
        }

        /// <summary>
        /// Processes the text file line by line and applies filters to each line.
        /// </summary>
        /// <param name="filePath">The path to the text file.</param>
        public async Task ProcessFileAsync(string filePath)
        {
            // Read the file line by line
            using var reader = new StreamReader(filePath);
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                // Apply filters to each line and output the result
                string resultText = ApplyFilters(line);
                Console.WriteLine(resultText);
            }
        }
    }
}