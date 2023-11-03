
namespace Feature.Product;

public class DeleteProductInput
{
    public string Id { get; }
    public DeleteProductInput(string id)
    {
        Id = id;
    }
    
}