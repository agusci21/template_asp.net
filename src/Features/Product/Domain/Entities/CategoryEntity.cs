namespace Feature.Product;

public class CategoryEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public CategoryEntity(string id, string name)
    {
        Id = id;
        Name = name;
    }
}