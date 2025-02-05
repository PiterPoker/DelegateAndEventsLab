using MaxFinderWithEvents; // Assuming this namespace contains extension methods GetMin and GetMax
using FileWalker;
using System.Collections.Generic;

namespace DelegateAndEventsLab;
internal class Program
{
    private static void Main(string[] args)
    {
        IArgumentsParser parser = new CommandLineArgumentsParser();
        var arguments = parser.Parse(args);

        var presenter = new ResultPresenter();

        if (arguments.TryGetValue("--task", out var task))
            switch (task)
            {
                case "Task1":
                    RunTask1(arguments, presenter);
                    break;
                case "Task2":
                    RunTask2(arguments, presenter);
                    break;
                default:
                    throw new ArgumentException(nameof(task));
            }
        Console.ReadLine();
    }


    /// <summary>
    /// Executes Task1, which involves generating random products and finding the most expensive and cheapest ones.
    /// </summary>
    /// <param name="arguments">Parsed command-line arguments containing product generation parameters.</param>
    /// <param name="presenter">An instance of <see cref="IProductsPresenter"/> to display product information.</param>
    private static void RunTask1(IDictionary<string, string> arguments, IProductsPresenter presenter)
    {
        if (!arguments.TryGetValue("--count", out var countString))
            return;
        int.TryParse(countString, out var count);
        if (!arguments.TryGetValue("--minprice", out var minpriceString))
            return;
        float.TryParse(minpriceString, out var minPrice);
        if (!arguments.TryGetValue("--maxprice", out var maxPriceString))
            return;
        float.TryParse(maxPriceString, out var maxPrice);

        var products = GenerateRandomProducts(count, minPrice, maxPrice);
        presenter.InitParamsDisplay(products);

        Product mostExpensiveProduct = products.GetMax(p => p.Price);

        Product cheapestProduct = products.GetMin(p => p.Price);
        presenter.ResultDisplay(mostExpensiveProduct, cheapestProduct);
    }

    /// <summary>
    /// Generates a list of products with random prices within a specified range.
    /// </summary>
    /// <param name="count">The number of products to generate.</param>
    /// <param name="minPrice">The minimum possible price for a product.</param>
    /// <param name="maxPrice">The maximum possible price for a product.</param>
    /// <returns>The list of generated products.</returns>
    private static List<Product> GenerateRandomProducts(int count, float minPrice, float maxPrice)
    {
        var random = new Random();
        var productNames = new string[]
        {
            "Smartphone",
            "Laptop",
            "Tablet",
            "Smartwatch",
            "Wireless headphones",
            "Game console",
            "Camera",
            "Printer",
            "Monitor",
            "Keyboard"
        };

        var products = new List<Product>();

        for (int i = 0; i < count; i++)
        {
            string name = productNames[random.Next(productNames.Length)];
            float price = (float)(random.NextDouble() * (maxPrice - minPrice) + minPrice);
            price = (float)Math.Round(price, 2);

            products.Add(new Product(name, price));
        }

        return products;
    }

    /// <summary>
    /// Executes Task2, which involves searching for files in a directory based on a search pattern.
    /// Displays the found files using the provided presenter.
    /// </summary>
    /// <param name="arguments">Parsed command-line arguments containing file search parameters.</param>
    /// <param name="presenter">An instance of <see cref="IFilesPresenter"/> to display file search results.</param>
    private static void RunTask2(IDictionary<string, string> arguments, IFilesPresenter presenter)
    {

        if (!arguments.TryGetValue("--directory", out var directory) ||
            !arguments.TryGetValue("--searchpattern", out var searchPattern))
        {
            Console.WriteLine("Parameters must be specified --directory и --searchpattern.");
            return;
        }

        bool searchAll = true;
        if (arguments.TryGetValue("--all", out var allValue))
        {
            bool.TryParse(allValue, out searchAll);
        }

        var fileSearcher = new FileSearcher(directory, searchPattern, searchAll);
        var foundFiles = new List<string>();

        fileSearcher.FileFound += (sender, e) =>
        {
            foundFiles.Add(e.Path);
            e.Cancel = searchAll;
        };

        fileSearcher.SearchCompleted += (sender, e) =>
        {
            Console.WriteLine("Search completed.");
        };

        fileSearcher.Search();
        presenter.ResultDisplay(foundFiles);

    }
}
