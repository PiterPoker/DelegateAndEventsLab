namespace DelegateAndEventsLab;
/// <summary>
/// Класс, представляющий продукт с именем и ценой.
/// </summary>
public record Product
{
    /// <summary>
    /// Название продукта.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена продукта.
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Product"/>.
    /// </summary>
    /// <param name="name">Название продукта.</param>
    /// <param name="price">Цена продукта.</param>
    public Product(string name, float price)
    {
        Name = name;
        Price = price;
    }
}