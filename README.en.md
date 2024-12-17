# DelegateAndEventsLab

This project demonstrates the use of delegates and events in C# to implement file searching, as well as finding the minimum and maximum path lengths among the found files.

## Description

The program takes command-line parameters specifying the directory to search and the file name pattern. It uses the `FileSearcher` class to recursively traverse directories and generate events when each file is found. Event handlers are used to collect information about the found files and manage the search process. After the search is complete, the program outputs a list of found files, as well as the minimum and maximum path lengths among them.

## Functionality

*   Search for files matching a given pattern in the specified directory and its subdirectories.
*   Use of `FileFoundEvent` and `PostProcessingEvent` events to handle found files and manage the search process.
*   Ability to cancel the search after finding the first file (default) or continue searching for all files (using the `--all true` parameter).
*   Finding the minimum and maximum path lengths among the found files using the `GetMin` and `GetMax` extension methods.
*   Handling `UnauthorizedAccessException` and `PathTooLongException` exceptions during directory traversal.
*   Parsing command-line arguments using a dictionary.

## Usage

To run the program, you need to use the command line in the following format:

DelegateAndEventsLab.exe --directory <directory_path> --searchpattern <search_pattern> [--all <true/false>]


*   `--directory`: Required parameter. The path to the directory where the search will be performed.
*   `--searchpattern`: Required parameter. The file search pattern (e.g., `*.txt`, `MyFile*.cs`).
*   `--all`: Optional parameter. If set to `true`, all files matching the pattern will be found. If `false` (default), the search will stop after the first file is found.

**Example:**

DelegateAndEventsLab.exe --directory E:\MyProjects --searchpattern *.cs --all true


## Output

The program outputs the following messages to the console:

*   A message about each found file in the format "File found: <file_name> in folder: <full_path>".
*   Minimum path length: "Min path: <minimum_path>".
*   Maximum path length: "Max path: <maximum_path>".

If no files are found, the program outputs the message "Files not found".

## Project Structure

*   `DelegateAndEventsLab`: The main project containing the `Program` class.
*   `FileWalker`: The project containing the `FileSearcher` class and event models (`FileArgs`, `FileSearcherArgs`).
*   `MaxFinderWithEvents`: The project containing the `GetMin` and `GetMax` extension methods.

## Dependencies

*   .NET Runtime (version specified in the project file), .NET 8 is recommended

## Classes and Events

*   `FileSearcher`: The class that performs the file search and generates events.
    *   `FileFoundEvent`: Event generated when a file is found.
    *   `PostProcessingEvent`: Event generated after processing a file or directory.
*   `FileArgs`: The event arguments class for the `FileFoundEvent`. Contains information about the file (`FullPath`, `FileName`, `Path`, `Cancel`).
*   `FileSearcherArgs`: The event arguments class for the `PostProcessingEvent`. Inherits from `FileArgs` and contains additional information about the search process (`CurrentDirectory`, `OldDirectory`, `Pattern`).
*   `EnumerableExtensions`: The class containing the `GetMin` and `GetMax` extension methods for `IEnumerable<T>`.

## Development Environment

*   **IDE:** Visual Studio Code
*   **.NET Version:** .NET 8

## Usage (Running)

1.  Clone the repository.
2.  Open the project in Visual Studio Code.
3.  Make sure you have the .NET 8 SDK installed. You can download it from the [official Microsoft website](https://dotnet.microsoft.com/download).
4.  Open the terminal in Visual Studio Code and navigate to the project directory containing the `.csproj` file.
5.  Execute the following commands:

    ```sh
    dotnet build
    dotnet run -- --directory <directory_path> --searchpattern <search_pattern> [--all <true/false>]
    ```

    Replace `<directory_path>` and `<search_pattern>` with the desired values. For example:

    ```sh
    dotnet run -- --directory E:\MyProjects --searchpattern *.cs --all true
    ```

6.  Check the console output.
