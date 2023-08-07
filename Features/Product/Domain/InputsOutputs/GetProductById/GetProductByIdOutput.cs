
using DTOs;

namespace Feature.Product;

public class GetProductByIdOutput
{
    public ProductDTO? Product { get;}

    public GetProductByIdOutput(ProductDTO? product)
    {
        Product = product;
    }

}