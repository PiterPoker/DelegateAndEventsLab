using MaxFinderWithEvents; // Assuming this namespace contains extension methods GetMin and GetMax
using FileWalker;
using System.Collections.Generic;

namespace DelegateAndEventsLab;

/// <summary>
/// The main program class for the DelegateAndEventsLab application.  This program demonstrates file searching using events and delegates, 
/// and also finds the minimum and maximum path lengths among found files.
/// </summary>
internal class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// Parses command-line arguments, performs a file search, displays results, and finds minimum and maximum file path lengths.
    /// </summary>
    /// <param name="args">Command-line arguments.  Expected format:  "--directory" "path" "--searchpattern" "*.txt"  [--all "true" | "false"]</param>
    private static void Main(string[] args)
    {
        var dictionary = ArrayToDictionary(args); //Parses command-line arguments into a dictionary.
        List<string> fullPathFiles = FileSearcher(dictionary); //Performs the file search.
        ShowResult(fullPathFiles); //Displays the list of found files.
        if (fullPathFiles.Count > 1)
            GetMinAndMaxPath(fullPathFiles); //Finds and displays the minimum and maximum path lengths.
        Console.Read(); //Keeps the console window open until a key is pressed.
    }

    /// <summary>
    /// Finds and displays the minimum and maximum path lengths from a list of file paths.
    /// </summary>
    /// <param name="fullPathFiles">A list of full file paths.</param>
    private static void GetMinAndMaxPath(List<string> fullPathFiles)
    {
        var appSettingsMin = fullPathFiles.GetMin(fp => fp.Length); //Uses an extension method to find the minimum path length.
        var appSettingsMax = fullPathFiles.GetMax(fp => fp.Length); //Uses an extension method to find the maximum path length.
        Console.WriteLine("Min path: {0}\nMax path: {1}\n", appSettingsMin, appSettingsMax);
    }

    /// <summary>
    /// Displays the list of found files to the console.
    /// </summary>
    /// <param name="fullPathFiles">A list of full file paths.</param>
    private static void ShowResult(List<string> fullPathFiles)
    {
        foreach (var pathFile in fullPathFiles)
        {
            Console.WriteLine($"Найден файл в папке: {pathFile}"); //Prints each file path.  (Найден файл в папке means "File found in folder")
        }
    }

    /// <summary>
    /// Performs a file search using the FileSearcher class and handles events.
    /// </summary>
    /// <param name="dictionary">A dictionary containing command-line arguments.</param>
    /// <returns>A list of full file paths found during the search.</returns>
    private static List<string> FileSearcher(Dictionary<string, string> dictionary)
    {
        FileSearcher fileSearcher = new();
        var fullPathFiles = new List<string>();

        // Event handler for file found events.  Logs the found file and adds its full path to the list.
        fileSearcher.FileFoundEvent += (sender, e) =>
        {
            var fileSearcher = sender as FileSearcher;
            fileSearcher?.OnWriteTextInfoHandler($"Найден файл: {e.FileName}"); //Logs the filename.
            fullPathFiles.Add(e.FullPath); //Adds the full path to the results list.
            fileSearcher?.OnWriteTextInfoHandler($"Записан полный путь к файлу: {e.FullPath}"); //Logs the full path.
        };

        // Event handler for post-processing events.  Allows canceling the search based on the "--all" command-line argument.
        fileSearcher.PostProcessingEvent += (sender, e) =>
        {
            bool isAll = true;
            if (dictionary.TryGetValue("--all", out var allString) && bool.TryParse(allString, out isAll))
            {
                e.Cancel = !isAll; //If --all is true, don't cancel. If false, cancel after the first file.
            }
            else
            {
                e.Cancel = isAll; // Default: Cancel after the first file.
            }
        };

        // Initiates the file search if the necessary command-line arguments are present.
        if (dictionary.TryGetValue("--directory", out var directiry) && dictionary.TryGetValue("--searchpattern", out var searchPattern))
        {
            fileSearcher.Search(directiry, searchPattern);
        }

        return fullPathFiles;
    }

    /// <summary>
    /// Parses command-line arguments into a key-value pair dictionary.
    /// </summary>
    /// <param name="array">The command-line argument array.</param>
    /// <returns>A dictionary where keys are arguments (e.g., "--directory") and values are their corresponding values.</returns>
    private static Dictionary<string, string> ArrayToDictionary(string[] array)
    {
        Dictionary<string, string> dictionary = new();
        for (int i = 0; i < array.Length; i += 2)
        {
            if (i + 1 < array.Length)
            {
                dictionary[array[i]] = array[i + 1];
            }
        }
        return dictionary;
    }
}