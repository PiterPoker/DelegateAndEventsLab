using MaxFinderWithEvents; // Assuming this namespace contains extension methods GetMin and GetMax
using FileWalker;
using System.Collections.Generic;
namespace DelegateAndEventsLab;

public class ResultPresenter : IProductsPresenter, IFilesPresenter
{
    public void InitParamsDisplay(IEnumerable<Product> products)
    {
        Console.WriteLine("Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name}: {product.Price:F2} $");
        }
    }
    public void ResultDisplay(Product mostExpensiveProduct, Product cheapestProduct)
    {
        Console.WriteLine($"The cheapest product: {cheapestProduct.Name} - {cheapestProduct.Price:F2} $");        
        Console.WriteLine($"\nThe most expensive product: {mostExpensiveProduct.Name} - {mostExpensiveProduct.Price:F2} $");
    }

    public void ResultDisplay(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            Console.WriteLine($"Found file in folder: {file}");
        }
    }
}

public interface IProductsPresenter
{
    public void InitParamsDisplay(IEnumerable<Product> products);
    public void ResultDisplay(Product mostExpensiveProduct, Product cheapestProduct);
}

public interface IFilesPresenter
{
    public void ResultDisplay(IEnumerable<string> files);    
}