using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TextFilterApp.Filters;

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
        // Normalize text to lower case
        input = input.ToLower();

        // Split the input text into words based on whitespace and punctuation characters.
        var words = Regex.Split(input, @"[\s\p{P}]+", RegexOptions.Compiled);

        // Apply each filter in sequence to the list of words.
        foreach (var filter in _filters)
        {
            words = filter.Apply(words).ToArray();

            // Exit early if there are no more words left after applying a filter.
            if (words.Length == 0)
            {
                return string.Empty;
            }
        }

        // Join the filtered words back into a single string and return it.
        return string.Join(" ", words);
    }
}