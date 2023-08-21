namespace Feature.Category;

public class ProductEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }

    public ProductEntity(string id, string name, int stock, double price)
    {
        Id = id;
        Name = name;
        Stock = stock;
        Price = price;
    } 
}