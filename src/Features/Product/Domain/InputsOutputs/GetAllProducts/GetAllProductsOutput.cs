namespace Feature.Product;

public class GetAllProductOutput
{
    public IEnumerable<ProductEntity> Products { get;}

    public GetAllProductOutput (IEnumerable<ProductEntity>? products)
    {   
        Products = products ?? new List<ProductEntity>();
    }
}