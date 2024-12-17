namespace FileWalker;

/// <summary>
/// Provides methods for writing styled messages to the console.
/// </summary>
public static class ConsoleStyle
{
    /// <summary>
    /// Writes a message to the console with the specified color.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="color">The foreground color for the message.</param>
    private static void SetMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Writes a warning message to the console in dark yellow.
    /// </summary>
    /// <param name="message">The warning message to display.</param>
    public static void WriteWarning(string message)
    {
        SetMessage(message, ConsoleColor.DarkYellow);
    }

    /// <summary>
    /// Writes an informational message to the console in dark green.
    /// </summary>
    /// <param name="message">The informational message to display.</param>
    public static void WriteInfo(string message)
    {
        SetMessage(message, ConsoleColor.DarkGreen);
    }

    /// <summary>
    /// Writes an error message to the console in dark red.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    public static void WriteError(string message)
    {
        SetMessage(message, ConsoleColor.DarkRed);
    }
}