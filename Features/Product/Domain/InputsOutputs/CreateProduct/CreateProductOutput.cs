
namespace Feature.Product;

public class CreateProductOutput
{
    public ProductEntity? ProductEntity { get;}

    public string? Error { get;}

    public CreateProductOutput (ProductEntity? productEntity)
    {
        ProductEntity = productEntity;
    }

    public CreateProductOutput (string? error)
    {
        Error = error;
    }
}