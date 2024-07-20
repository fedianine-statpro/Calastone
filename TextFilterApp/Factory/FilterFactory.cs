using TextFilterApp.Configuration;
using TextFilterApp.Filters;

namespace TextFilterApp.Factory
{
    /// <summary>
    /// Factory class for creating filter instances based on the provided configuration.
    /// </summary>
    public class FilterFactory
    {
        /// <summary>
        /// Creates a list of filters based on the provided filter configuration.
        /// </summary>
        /// <param name="config">The filter configuration.</param>
        /// <returns>A list of filters based on the configuration.</returns>
        public static List<IFilter> CreateFilters(AppConfig.FilterConfig config)
        {
            var filters = new List<IFilter>();

            if (config.LengthFilter?.Enabled == true)
                filters.Add(new LengthFilter(config.LengthFilter.MinLength));

            if (config.CharacterFilter?.Enabled == true)
                filters.Add(new CharacterFilter(config.CharacterFilter.Characters));

            if (config.VowelMiddleFilter?.Enabled == true)
                filters.Add(new VowelMiddleFilter());

            if (config.RegexFilter?.Enabled == true)
                filters.Add(new RegexFilter(config.RegexFilter.Pattern));

            return filters;
        }
    }
}