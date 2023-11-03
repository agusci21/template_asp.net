
namespace Feature.Product;

public class GetProductByIdInput
{
    public string Id { get; }
    public GetProductByIdInput( string id )
    {
        Id = id;
    }
}