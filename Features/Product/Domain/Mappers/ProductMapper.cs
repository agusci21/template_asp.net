namespace Feature.Product;
using DTOs;

public static class ProductMapper
{

    public static ProductEntity FromDTO(ProductDTO productDTO)
    {
        var productEntity = new ProductEntity()
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Stock = productDTO.Stock,
            Price = productDTO.Price,
            ImageUrl = productDTO.ImageUrl
        };

        if (productDTO.Category != null)
        {
            productEntity.Category = CategoryMapper.FromDTO(productDTO.Category);
        }

        return productEntity;
    }
}