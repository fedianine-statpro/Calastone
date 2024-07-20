# Text Filter Application

## Overview
This application processes text files by applying a series of configurable filters to the text. The filters can remove words based on various criteria such as length, characters contained, regex patterns, and vowels in the middle of the word. The application is designed to be easily configurable and extensible.

## Features
- **Configurable Filters**: Define filters in a JSON configuration file.
- **Multiple Filter Types**: Length filter, character filter, regex filter, and vowel middle filter.
- **Asynchronous Processing**: Supports asynchronous file reading and processing.
- **Dependency Injection**: Uses dependency injection for filter configuration and instantiation.
- **Performance Considerations**: Early exit when no words are left after applying filters, and potential for parallel filter application.

## Prerequisites
- .NET 8.0 SDK or later
- A text editor or IDE that supports C# programming

## Configuration
The application uses a JSON configuration file (`appsettings.json`) to define the filters. Below is an example configuration:

```json
{
    "Filters": {
        "LengthFilter": {
            "Enabled": true,
            "MinLength": 5
        },
        "CharacterFilter": {
            "Enabled": true,
            "Characters": "t"
        },
        "VowelMiddleFilter": {
            "Enabled": true
        },
        "RegexFilter": {
            "Enabled": true,
            "Pattern": "^\\d+$"
        }
    }
}
```

## Usage
1. Place the text file to be processed in the same directory as the executable and name it `sample.txt`.
2. Place the configuration file in the same directory as the executable and name it `appsettings.json`.
3. Run the application:
   ```bash
   dotnet run
   ```

## Examples
### Input (`sample.txt`)
```
Alice was beginning to get very tired of sitting by her sister on the bank...
```

### Configuration (`appsettings.json`)
```json
{
    "Filters": {
        "LengthFilter": {
            "Enabled": true,
            "MinLength": 5
        },
        "CharacterFilter": {
            "Enabled": true,
            "Characters": "t"
        },
        "VowelMiddleFilter": {
            "Enabled": true
        }
    }
}
```

### Output
```
beginning
```

# Text Filtering Assumptions

## Punctuation and Symbols

1. **Punctuation as Word Delimiters**:
   - Punctuation marks (e.g., periods, commas, exclamation marks) are treated as delimiters. They separate words but are not included in the words themselves.
   - Example: "Hello, world!" becomes ["Hello", "world"].

2. **Symbols as Part of Words**:
   - Symbols (e.g., @, #, $, %) are considered part of words unless specified otherwise.
   - Example: "email@example.com" is treated as a single word.

## Considerations
This implementation is flexible and allows adding new filters easily. For example, you could add a filter to exclude words longer than a certain length or based on other criteria. To implement a new filter, create a class that implements the `IFilter` interface and add it to the configuration and the `TextProcessor`.

## Future Considerations
- **Parallel Filter Application**: For performance improvements, consider applying filters in parallel, especially if filters are independent of each other.
- **In-Memory File Processing**: Depending on the file size, it may make sense to read the entire file into memory to reduce I/O operations and improve performance. This should be configurable based on the expected file sizes and system memory constraints.

## Testing
Unit tests are provided to ensure the correctness of each filter and the overall text processing logic. The tests can be run using the `dotnet test` command.

## Conclusion
This text filter application provides a flexible and configurable way to process text files with various filters. Future enhancements can focus on parallel filter application and in-memory file processing for performance improvements based on the file size and system capabilities.