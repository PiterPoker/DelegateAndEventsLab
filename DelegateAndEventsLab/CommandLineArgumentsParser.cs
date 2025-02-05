using MaxFinderWithEvents; // Assuming this namespace contains extension methods GetMin and GetMax
using FileWalker;
using System.Collections.Generic;
namespace DelegateAndEventsLab;
public class CommandLineArgumentsParser : IArgumentsParser
{
    public IDictionary<string, string> Parse(string[] args)
    {
        var dictionary = new Dictionary<string, string>();
        for (int i = 0; i < args.Length; i += 2)
        {
            if (i + 1 < args.Length)
            {
                dictionary[args[i]] = args[i + 1];
            }
        }
        return dictionary;
    }
}
public interface IArgumentsParser
{
    IDictionary<string, string> Parse(string[] args);
}
