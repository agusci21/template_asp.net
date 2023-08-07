using Feature.Category;

namespace Feature.Product;
public class ProductEntity
{
    public required string Id { get; set;}
    public required string Name { get; set;}
    public int Stock { get; set;}
    public double Price { get; set;}
    public CategoryEntity? Category { get; set;}
    public string? ImageUrl { get; set;}

    public ProductEntity(){}
}