using FileWalker.Models;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FileWalker;
/// <summary>
/// Provides functionality to recursively search for files within a specified directory and its subdirectories.
/// Uses events to notify subscribers about file findings and post-processing.
/// Handles potential exceptions like unauthorized access and excessively long paths.
/// </summary>
public class FileSearcher
{
    /// <summary>
    /// Event triggered when a file matching the search criteria is found.
    /// </summary>
    public event EventHandler<FileArgs>? FileFoundEvent;

    /// <summary>
    /// Event triggered after processing a file or directory (useful for post-processing tasks).
    /// </summary>
    public event EventHandler<FileSearcherArgs>? PostProcessingEvent;

    /// <summary>
    /// Writes an informational message to the console.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public virtual void OnWriteTextInfoHandler(string message) => ConsoleStyle.WriteInfo($"Info: {message}");


    /// <summary>
    /// Writes a warning message to the console.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public virtual void OnWriteTextWarningHandler(string message) => ConsoleStyle.WriteWarning($"Warning: {message}");

    /// <summary>
    /// Writes an error message to the console.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public virtual void OnWriteTextErrorHandler(string message) => ConsoleStyle.WriteError($"Error: {message}");

    /// <summary>
    /// Raises the FileFoundEvent event.
    /// </summary>
    /// <param name="e">The FileArgs object containing file information.</param>
    protected virtual void OnFileFoundEvent(FileArgs e) => FileFoundEvent?.Invoke(this, e);

    /// <summary>
    /// Raises the PostProcessingEvent event.
    /// </summary>
    /// <param name="e">The FileSearcherArgs object containing search information.</param>
    protected virtual void OnPostProcessingEvent(FileSearcherArgs e) => PostProcessingEvent?.Invoke(this, e);

    /// <summary>
    /// Initiates a file search within the specified directory.
    /// </summary>
    /// <param name="directory">The directory to start the search from.</param>
    /// <param name="searchPattern">The search pattern for files (e.g., "*.txt").</param>
    /// <param name="cancel">A flag to cancel the search operation.</param>
    public virtual void Search(string directory, string searchPattern, bool cancel = false)
    {
        SearchFiles(new FileSearcherArgs(directory, searchPattern, cancel));
    }

    /// <summary>
    /// Recursively searches for files matching the specified pattern.
    /// </summary>
    /// <param name="e">FileSearcherArgs containing search parameters and state.</param>
    protected virtual void SearchFiles(FileSearcherArgs e)
    {
        if (!SearchForFile(e))
            SearchForSubDirectory(e);
    }

    /// <summary>
    /// Searches for files in the current directory.
    /// </summary>
    /// <param name="e">FileSearcherArgs containing search parameters and state.</param>
    /// <returns>True if any error occurred or search was cancelled; otherwise, False.</returns>
    private bool SearchForFile(FileSearcherArgs e)
    {
        try
        {
            string[] files = Directory.GetFiles(e.CurrentDirectory, e.Pattern);
            foreach (string file in files)
            {
                e.FullPath = file;
                OnFileFoundEvent(e);
                OnPostProcessingEvent(e);
                if (e.Cancel) return true;
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            OnWriteTextWarningHandler($"File access error in the directory: {ex.Message}");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Recursively searches subdirectories for files.
    /// </summary>
    /// <param name="e">FileSearcherArgs containing search parameters and state.</param>
    private void SearchForSubDirectory(FileSearcherArgs e)
    {
        foreach (string dir in Directory.GetDirectories(e.CurrentDirectory))
        {
            try
            {
                e.CurrentDirectory = dir;
                SearchFiles(e);
                if (e.Cancel) return;
            }
            catch (UnauthorizedAccessException)
            {
                OnWriteTextWarningHandler($"Skipping directory with restricted access: {dir}");
            }
            catch (PathTooLongException)
            {
                OnWriteTextErrorHandler($"Skipping directory due to excessively long path: {dir}");
            }
        }
    }
}
