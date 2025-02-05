using MaxFinderWithEvents; // Assuming this namespace contains extension methods GetMin and GetMax
using FileWalker;
using System.Collections.Generic;

namespace DelegateAndEventsLab;
internal class Program
{
    private static void Main(string[] args)
    {
        // Парсинг аргументов командной строки
        IArgumentsParser parser = new CommandLineArgumentsParser();
        var arguments = parser.Parse(args);
        if (arguments.TryGetValue("--task", out var task))
        switch (task)
        {
            case "Task1":
                RunTask1(arguments);
            break;
            case "Task2":
                RunTask2(arguments);
            break;
            default:
            throw new ArgumentException(nameof(task));
        }
        Console.ReadLine();
    }

    private static void RunTask1(IDictionary<string, string> arguments)
    {

    }

    private static void RunTask2(IDictionary<string, string> arguments)
    {

        if (!arguments.TryGetValue("--directory", out var directory) ||
            !arguments.TryGetValue("--searchpattern", out var searchPattern))
        {
            Console.WriteLine("Необходимо указать параметры --directory и --searchpattern.");
            return;
        }

        bool searchAll = true;
        if (arguments.TryGetValue("--all", out var allValue))
        {
            bool.TryParse(allValue, out searchAll);
        }

        // Поиск файлов
        var fileSearcher = new FileSearcher(directory, searchPattern, searchAll);
        var foundFiles = new List<string>();

        fileSearcher.FileFound += (sender, e) =>
        {
            foundFiles.Add(e.Path);
            e.Cancel = searchAll;
        };

        fileSearcher.SearchCompleted += (sender, e) =>
        {
            Console.WriteLine("Поиск завершен.");
        };

        fileSearcher.Search();

        // Отображение результатов
        var presenter = new ResultPresenter();
        presenter.DisplayFiles(foundFiles);

    }
}
