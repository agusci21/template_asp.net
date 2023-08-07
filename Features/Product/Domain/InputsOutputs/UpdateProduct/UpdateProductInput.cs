namespace Feature.Product;

public class UpdateProductInput
{
    public string? Id { get; set;}
    public string? Name {get;}
    public string? CategoryId{get;}
    public double? Price{get;}
    public int? Stock{get;}

    public string? ImageUrl{get;}

    public UpdateProductInput(string? name, string? categoryId, double? price, int? stock, string? imageUrl) 
    {
        Name = name;
        CategoryId = categoryId;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
    }

    public void SetId (string id)
    {
        Id = id;
    }
    
}