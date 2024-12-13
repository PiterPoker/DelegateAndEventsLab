using FileWalker.Models;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FileWalker;
/// <summary>
/// Provides functionality to search for files within a directory and its subdirectories.
/// Uses events to notify subscribers about file findings and post-processing.
/// Handles potential exceptions like unauthorized access and excessively long paths.
/// </summary>
public class FileSearcher
{
    private delegate void WriteTextHandler(string message);    
    private WriteTextHandler? _writeText;
    /// <summary>
    /// Event triggered when a file matching the search criteria is found.
    /// </summary>
    public event EventHandler<FileArgs>? FileFoundEvent;
    /// <summary>
    /// Event triggered after processing a file or directory (useful for post-processing tasks).
    /// </summary>
    public event EventHandler<FileSearcherArgs>? PostProcessingEvent;

    /// <summary>
    /// Internal helper to write informational messages to the console.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public virtual void OnWriteTextInfoHandler(string message)
    {
        _writeText = ConsoleStyle.WriteInfo;
        _writeText?.Invoke(string.Format("Info: {0}", message));
    }

    /// <summary>
    /// Internal helper to write warning messages to the console.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public virtual void OnWriteTextWarningHandler(string message)
    {
        _writeText = ConsoleStyle.WriteWarning;
        _writeText?.Invoke(string.Format("Warning: {0}", message));
    }

    /// <summary>
    /// Internal helper to write error messages to the console.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public virtual void OnWriteTextErrorHandler(string message)
    {
        _writeText = ConsoleStyle.WriteError;
        _writeText?.Invoke(string.Format("Error: {0}", message));
    }

    /// <summary>
    /// Raises the FileFoundEvent event.
    /// </summary>
    /// <param name="e">The FileArgs object containing file information.</param>
    public virtual void OnFileFoundEvent(FileArgs e) => FileFoundEvent?.Invoke(this, e);

    /// <summary>
    /// Raises the PostProcessingEvent event.
    /// </summary>
    /// <param name="e">The FileSearcherArgs object containing search information.</param>
    public virtual void OnPostProcessingEvent(FileSearcherArgs e) => PostProcessingEvent?.Invoke(this, e);

    /// <summary>
    ///  Initiates a file search within the specified directory.
    /// </summary>
    /// <param name="directory">The directory to start the search from.</param>
    /// <param name="searchPattern">The search pattern for files (e.g., "*.txt").</param>
    /// <param name="cancel">A flag to cancel the search operation.</param>
    public virtual void Search(string directory, string searchPattern, bool cancel = false)
    {
        SearchFiles(new FileSearcherArgs(directory, searchPattern, cancel));
    }

    public void SearchFiles(FileSearcherArgs e)
    {
        SearchForFile(e);
            SearchForSubDirectory(e);
    }
    
    /// <summary>
    /// Searches for files in the current directory.
    /// </summary>
    /// <param name="e">FileSearcherArgs object containing search parameters.</param>
    private void SearchForFile(FileSearcherArgs e)
    {
        try
        {

            var files = Directory.GetFiles(e.CurrentDirectory, e.FileName);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    OnFileFoundEvent(e);
                    OnPostProcessingEvent(e);
                    if (e.Cancel)
                    {
                        return;
                    }
                }
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            OnWriteTextWarningHandler($"Error accessing files in the directory: {ex.Message}");
        }
    }

    /// <summary>
    ///  Handles the recursive search for subdirectories.
    /// </summary>
    /// <param name="e">FileSearcherArgs object containing search parameters.</param>
    private void SearchForSubDirectory(FileSearcherArgs e)
    {
        foreach (var dir in Directory.GetDirectories(e.CurrentDirectory))
        {
            try
            {
                e.CurrentDirectory = dir;
                SearchFiles(e);
                if (e.Cancel)
                {
                    return;
                }
            }
            catch (UnauthorizedAccessException)
            {
                OnWriteTextWarningHandler($"Skipping directory with restricted access: {dir}");
            }
            catch (PathTooLongException)
            {
                OnWriteTextErrorHandler($"Skipping directory due to path too long: {dir}");
            }
        }
    }

}
