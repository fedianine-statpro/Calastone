using Xunit;
using TextFilterApp.Factory;
using Moq;
using TextFilterApp.Filters;
using TextFilterApp.Configuration;

namespace TextFilterApp.Tests
{
    /// <summary>
    /// Unit tests for the TextProcessor class.
    /// </summary>
    public class TextProcessorTests
    {
        /// <summary>
        /// Helper method to create a TextProcessor with the specified filter configuration.
        /// </summary>
        /// <param name="filterConfig">The filter configuration.</param>
        /// <returns>A configured TextProcessor instance.</returns>
        private TextProcessor CreateTextProcessor(AppConfig.FilterConfig filterConfig)
        {
            var filters = FilterFactory.CreateFilters(filterConfig);
            return new TextProcessor(filters);
        }

        /// <summary>
        /// Tests that the VowelMiddleFilter removes words with vowels in the middle.
        /// </summary>
        [Fact]
        public void ApplyFilters_RemovesWordsWithVowelsInMiddle()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                VowelMiddleFilter = new AppConfig.FilterConfig.FilterConfigBase { Enabled = true }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "clean what currently the rather";
            var result = textProcessor.ApplyFilters(input);

            Assert.DoesNotContain("clean", result); // "clean" contains the vowel 'e' in the middle
            Assert.DoesNotContain("what", result); // "what" contains the vowel 'a' in the middle
            Assert.DoesNotContain("currently", result); // "currently" contains the vowel 'e' in the middle
            Assert.Contains("the", result); // "the" does not meet the filter criteria
            Assert.Contains("rather", result); // "rather" does not meet the filter criteria
        }

        /// <summary>
        /// Tests that the LengthFilter removes words shorter than the specified minimum length.
        /// </summary>
        [Fact]
        public void ApplyFilters_RemovesWordsShorterThanMinLength()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                LengthFilter = new AppConfig.FilterConfig.LengthFilterConfig { Enabled = true, MinLength = 5 }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "it is a test longer";
            var result = textProcessor.ApplyFilters(input);

            Assert.DoesNotContain("it", result); // "it" is shorter than the minimum length of 5
            Assert.DoesNotContain("is", result); // "is" is shorter than the minimum length of 5
            Assert.DoesNotContain("a", result); // "a" is shorter than the minimum length of 5
            Assert.DoesNotContain("test", result); // "test" is shorter than the minimum length of 5
            Assert.Contains("longer", result); // "longer" meets the filter criteria
        }

        /// <summary>
        /// Tests that the CharacterFilter removes words containing specified characters.
        /// </summary>
        [Fact]
        public void ApplyFilters_RemovesWordsContainingSpecifiedCharacters()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                CharacterFilter = new AppConfig.FilterConfig.CharacterFilterConfig { Enabled = true, Characters = "t" }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "this is a test";
            var result = textProcessor.ApplyFilters(input);

            Assert.DoesNotContain("this", result); // "this" contains the character 't'
            Assert.DoesNotContain("test", result); // "test" contains the character 't'
            Assert.Contains("is", result); // "is" does not meet the filter criteria
            Assert.Contains("a", result); // "a" does not meet the filter criteria
        }

        /// <summary>
        /// Tests that the RegexFilter removes words matching the specified regex pattern.
        /// </summary>
        [Fact]
        public void ApplyFilters_RemovesWordsMatchingRegexPattern()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                RegexFilter = new AppConfig.FilterConfig.RegexFilterConfig { Enabled = true, Pattern = @"^\d+$" }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "123 this 456 is a test 789";
            var result = textProcessor.ApplyFilters(input);

            Assert.DoesNotContain("123", result); // "123" matches the regex pattern
            Assert.DoesNotContain("456", result); // "456" matches the regex pattern
            Assert.DoesNotContain("789", result); // "789" matches the regex pattern
            Assert.Contains("this", result); // "this" does not meet the filter criteria
            Assert.Contains("is", result); // "is" does not meet the filter criteria
            Assert.Contains("a", result); // "a" does not meet the filter criteria
            Assert.Contains("test", result); // "test" does not meet the filter criteria
        }

        /// <summary>
        /// Tests that the filters can be chained together and applied in sequence.
        /// </summary>
        [Fact]
        public void ApplyFilters_ChainsFiltersCorrectly()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                VowelMiddleFilter = new AppConfig.FilterConfig.FilterConfigBase { Enabled = true },
                LengthFilter = new AppConfig.FilterConfig.LengthFilterConfig { Enabled = true, MinLength = 3 },
                CharacterFilter = new AppConfig.FilterConfig.CharacterFilterConfig { Enabled = true, Characters = "t" }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "clean what currently the rather is test allow";
            var result = textProcessor.ApplyFilters(input);

            Assert.DoesNotContain("clean", result); // "clean" contains the vowel 'e' in the middle
            Assert.DoesNotContain("what", result); // "what" contains the character 't'
            Assert.DoesNotContain("currently", result); // "currently" contains the character 't'
            Assert.DoesNotContain("is", result); // "is" is shorter than the minimum length of 3
            Assert.DoesNotContain("test", result); // "test" contains the character 't'
            Assert.DoesNotContain("the", result); // "the" contains the character 't'
            Assert.DoesNotContain("rather", result); // "rather" contains the character 't'

            Assert.Contains("allow", result); // "allow" does not meet any filter criteria
        }

        /// <summary>
        /// Tests that the filters are applied in sequence and early exit occurs when no words are left.
        /// </summary>
        [Fact]
        public void ApplyFilters_EarlyExitWhenNoWordsLeft()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                LengthFilter = new AppConfig.FilterConfig.LengthFilterConfig { Enabled = true, MinLength = 10 }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "short";
            var result = textProcessor.ApplyFilters(input);

            Assert.Equal(string.Empty, result); // All words should be filtered out due to length
        }

        /// <summary>
        /// Tests that the filters are called in the correct order using Moq.
        /// </summary>
        [Fact]
        public void ApplyFilters_CallsFiltersInOrder()
        {
            var mockFilter1 = new Mock<IFilter>();
            var mockFilter2 = new Mock<IFilter>();

            var orderList = new List<string>();

            mockFilter1.Setup(f => f.Apply(It.IsAny<string>())).Callback(() => orderList.Add("Filter1")).Returns<string>(word => true);
            mockFilter2.Setup(f => f.Apply(It.IsAny<string>())).Callback(() => orderList.Add("Filter2")).Returns<string>(word => true);

            var filters = new List<IFilter> { mockFilter1.Object, mockFilter2.Object };
            var textProcessor = new TextProcessor(filters);

            var input = "this is a test";
            var result = textProcessor.ApplyFilters(input);

            // Verify that filters are called in order for each word
            var expectedOrder = new List<string>
            {
                "Filter1", "Filter2", // for "this"
                "Filter1", "Filter2", // for "is"
                "Filter1", "Filter2", // for "a"
                "Filter1", "Filter2"  // for "test"
            };

            Assert.Equal(expectedOrder, orderList);

            mockFilter1.Verify(f => f.Apply(It.IsAny<string>()), Times.Exactly(4));
            mockFilter2.Verify(f => f.Apply(It.IsAny<string>()), Times.Exactly(4));
        }

        /// <summary>
        /// Tests that the TextProcessor handles an empty input string correctly.
        /// </summary>
        [Fact]
        public void ApplyFilters_HandlesEmptyInput()
        {
            var filterConfig = new AppConfig.FilterConfig
            {
                LengthFilter = new AppConfig.FilterConfig.LengthFilterConfig { Enabled = true, MinLength = 5 }
            };
            var textProcessor = CreateTextProcessor(filterConfig);

            var input = "";
            var result = textProcessor.ApplyFilters(input);

            Assert.Equal(string.Empty, result); // Result should be an empty string
        }
    }
}