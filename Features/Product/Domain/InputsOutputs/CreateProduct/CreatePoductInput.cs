
namespace Feature.Product;

public class CreateProductInput
{
    public string Name {get;}
    public string? CategoryId{get;}
    public double Price{get;}
    public int Stock{get;}

    public string? ImageUrl{get;}

    public CreateProductInput(string name,  double price, int stock, string? categoryId,string? imageUrl) 
    {
        Name = name;
        CategoryId = categoryId;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
    }
}