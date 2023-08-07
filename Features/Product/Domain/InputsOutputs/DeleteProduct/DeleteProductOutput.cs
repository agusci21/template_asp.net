
namespace Feature.Product;

public class DeleteProductOutput
{
    public ProductEntity? Product { get; }
    public string? Error {get;}

    public DeleteProductOutput(ProductEntity? product)
    {
        Product = product;
    }

    public DeleteProductOutput(string? error)
    {
        Error = error;
    }
}