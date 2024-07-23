using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextFilterApp.Configuration;
using TextFilterApp.Factory;

namespace TextFilterApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Hardcoded file paths for text and configuration files
                string textFilePath = "sample.txt";
                string configFilePath = "appsettings.json";

                // Check if the text file exists
                if (!File.Exists(textFilePath))
                {
                    Console.WriteLine("The provided text file path does not exist.");
                    return;
                }

                // Check if the configuration file exists
                if (!File.Exists(configFilePath))
                {
                    Console.WriteLine("The provided configuration file path does not exist.");
                    return;
                }

                // Set up dependency injection and configuration
                var serviceProvider = ConfigureServices(configFilePath);
                var textProcessor = serviceProvider.GetService<TextProcessor>();

                // Measure execution time
                var stopwatch = Stopwatch.StartNew();
                await textProcessor.ProcessFileAsync(textFilePath);
                stopwatch.Stop();

                // Output execution time
                Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Configures the services and dependency injection.
        /// </summary>
        /// <param name="configFilePath">The path to the configuration file.</param>
        /// <returns>A configured service provider.</returns>
        private static ServiceProvider ConfigureServices(string configFilePath)
        {
            // Build configuration from JSON file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFilePath)
                .Build();

            // Bind configuration to AppConfig object
            var config = new AppConfig();
            configuration.Bind(config);

            // Set up dependency injection
            var services = new ServiceCollection()
                .AddSingleton(config)
                .AddTransient<FilterFactory>()
                .AddTransient(provider =>
                {
                    var filters = FilterFactory.CreateFilters(config.Filters);
                    return new TextProcessor(filters);
                });

            return services.BuildServiceProvider();
        }
    }
}