using System.Collections.Generic;
using FileWalker.Models;

namespace FileWalker;
public interface IFileSearcher
{
    event EventHandler<FileArgs> FileFound;
    event EventHandler<FileSearcherArgs> SearchCompleted;
    void Search();
}

public class FileSearcher : IFileSearcher
{
    public event EventHandler<FileArgs> FileFound;
    public event EventHandler<FileSearcherArgs> SearchCompleted;

    private readonly string _directory;
    private readonly string _searchPattern;
    private readonly bool _searchAll;

    public FileSearcher(string directory, string searchPattern, bool searchAll)
    {
        _directory = directory;
        _searchPattern = searchPattern;
        _searchAll = searchAll;
    }

    public void Search()
    {
        var args = new FileSearcherArgs(_directory, _searchPattern);
        SearchDirectory(args);

        // По окончании поиска вызываем событие SearchCompleted
        OnSearchCompleted(args);
    }

    protected virtual void OnFileFound(FileArgs e)
    {
        FileFound?.Invoke(this, e);
    }

    protected virtual void OnSearchCompleted(FileSearcherArgs e)
    {
        SearchCompleted?.Invoke(this, e);
    }

    private void SearchDirectory(FileSearcherArgs e)
    {
        if (e.Cancel) return;

        try
        {
            string[] files = Directory.GetFiles(e.CurrentDirectory, e.Pattern);
            foreach (var file in files)
            {
                e.FullPath = file;
                OnFileFound(new FileArgs(file));

                if (!_searchAll)
                {
                    e.Cancel = true;
                    return;
                }
            }

            string[] directories = Directory.GetDirectories(e.CurrentDirectory);
            foreach (var dir in directories)
            {
                e.CurrentDirectory = dir;
                SearchDirectory(e);
                if (e.Cancel) return;
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Недостаточно прав для доступа к директории {e.CurrentDirectory}: {ex.Message}");
        }
    }
}
