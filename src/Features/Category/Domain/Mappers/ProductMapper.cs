namespace Feature.Category;
using DTOs;

public static class ProductMapper
{

    public static ProductEntity FromDTO(ProductDTO productDTO)
    {
        var productEntity = new ProductEntity(
        
            id: productDTO.Id,
            name: productDTO.Name,
            stock: productDTO.Stock,
            price: productDTO.Price
        );

        return productEntity;
    }
}