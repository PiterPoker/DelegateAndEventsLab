using MaxFinderWithEvents; // Assuming this namespace contains extension methods GetMin and GetMax
using FileWalker;
using System.Collections.Generic;
namespace DelegateAndEventsLab;
public class ResultPresenter
{
    public void DisplayFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            Console.WriteLine($"Найден файл: {file}");
        }
    }
}
