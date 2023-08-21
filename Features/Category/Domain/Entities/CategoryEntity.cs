namespace Feature.Category;

public class CategoryEntity
{
    public string Id { get;}
    public string Name { get;}
    public IEnumerable<ProductEntity>? Products { get;}
    public CategoryEntity (string id, string name, IEnumerable<ProductEntity>? products)
    {
        Id = id;
        Name = name;
        Products = products;
    }

}
