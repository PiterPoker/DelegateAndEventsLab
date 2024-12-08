using System.Runtime.InteropServices;

namespace MaxFinderWithEvents;

public static class EnumerableExtensions
{
    public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
    {
        if (collection == null || !collection.Any())
            throw new ArgumentException("The collection cannot be null or empty");

        var enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("The collection cannot be empty");

        T maxItem = enumerator.Current;
        float maxValue = convertToNumber(maxItem);

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            var value = convertToNumber(item);
            if (value > maxValue)
            {
                maxValue = value;
                maxItem = item;
            }
        }

        return maxItem;
    }
}
