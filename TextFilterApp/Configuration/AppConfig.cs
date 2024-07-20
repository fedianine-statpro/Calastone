using System.Text.Json;

namespace TextFilterApp.Configuration
{
    /// <summary>
    /// Class responsible for loading and holding application configuration.
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Gets or sets the filter configurations.
        /// </summary>
        public FilterConfig Filters { get; set; }

        /// <summary>
        /// Configuration class that holds settings for all types of filters.
        /// </summary>
        public class FilterConfig
        {
            /// <summary>
            /// Gets or sets the configuration for the LengthFilter.
            /// </summary>
            public LengthFilterConfig LengthFilter { get; set; }

            /// <summary>
            /// Gets or sets the configuration for the CharacterFilter.
            /// </summary>
            public CharacterFilterConfig CharacterFilter { get; set; }

            /// <summary>
            /// Gets or sets the configuration for the VowelMiddleFilter.
            /// </summary>
            public FilterConfigBase VowelMiddleFilter { get; set; }

            /// <summary>
            /// Gets or sets the configuration for the RegexFilter.
            /// </summary>
            public RegexFilterConfig RegexFilter { get; set; }

            /// <summary>
            /// Base configuration class for filters.
            /// Provides common properties shared by all filter configurations.
            /// </summary>
            public class FilterConfigBase
            {
                /// <summary>
                /// Gets or sets a value indicating whether the filter is enabled.
                /// </summary>
                public bool Enabled { get; set; }
            }

            /// <summary>
            /// Configuration class for the CharacterFilter.
            /// Inherits common filter configuration properties from FilterConfigBase.
            /// </summary>
            public class CharacterFilterConfig : FilterConfigBase
            {
                /// <summary>
                /// Gets or sets the characters used for filtering words.
                /// Words containing any of these characters will be filtered out.
                /// </summary>
                public string Characters { get; set; }
            }

            /// <summary>
            /// Configuration class for the LengthFilter.
            /// Inherits common filter configuration properties from FilterConfigBase.
            /// </summary>
            public class LengthFilterConfig : FilterConfigBase
            {
                /// <summary>
                /// Gets or sets the minimum length required for words to pass the filter.
                /// </summary>
                public int MinLength { get; set; }
            }

            /// <summary>
            /// Configuration class for the RegexFilter.
            /// Inherits common filter configuration properties from FilterConfigBase.
            /// </summary>
            public class RegexFilterConfig : FilterConfigBase
            {
                /// <summary>
                /// Gets or sets the regex pattern used for filtering words.
                /// </summary>
                public string Pattern { get; set; }
            }
        }
    }
}