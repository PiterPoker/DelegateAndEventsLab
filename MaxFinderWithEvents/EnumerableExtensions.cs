using System.Runtime.InteropServices;

namespace MaxFinderWithEvents;
/// <summary>
/// Provides extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Finds the item in the collection that yields the maximum value when passed through a conversion function.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection. Must be a class.</typeparam>
    /// <param name="collection">The input collection.</param>
    /// <param name="convertToNumber">A function that converts an item of type <typeparamref name="T"/> to a float value.</param>
    /// <returns>The item in the collection that produces the maximum converted value.</returns>
    /// <exception cref="ArgumentException">Thrown if the collection is null or empty.</exception>
    public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
    {
        if (collection == null)
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

    /// <summary>
    /// Finds the item in the collection that yields the minimum value when passed through a conversion function.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection. Must be a class.</typeparam>
    /// <param name="collection">The input collection.</param>
    /// <param name="convertToNumber">A function that converts an item of type <typeparamref name="T"/> to a float value.</param>
    /// <returns>The item in the collection that produces the minimum converted value.</returns>
    /// <exception cref="ArgumentException">Thrown if the collection is null or empty.</exception>

    public static T GetMin<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
    {
        if (collection == null)
            throw new ArgumentException("The collection cannot be null or empty");

        var enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("The collection cannot be empty");

        T minItem = enumerator.Current;
        float minValue = convertToNumber(minItem);

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            var value = convertToNumber(item);
            if (value < minValue)
            {
                minValue = value;
                minItem = item;
            }
        }

        return minItem;
    }
}
