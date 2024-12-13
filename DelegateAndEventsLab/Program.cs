using System.Runtime.CompilerServices;
using MaxFinderWithEvents;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Rectangle> rectangles =
        [
            new Rectangle(5.5f, 3.2f),
            new Rectangle(7.1f, 4.8f),
            new Rectangle(2.2f, 2.2f),
            new Rectangle(10.0f, 6.5f),
            new Rectangle(1.5f, 9.9f),
        ];
        var rectangle = rectangles.GetMax(c => c.Perimeter);
        Console.WriteLine("Hello, World!");
    }
}
