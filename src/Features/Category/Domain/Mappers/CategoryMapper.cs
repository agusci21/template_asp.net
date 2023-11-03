namespace Feature.Category;
using DTOs;
public static class CategoryMapper
{
    public static CategoryEntity FromDTO (CategoryDTO categoryDTO)
    {
        var products = categoryDTO.Products.Select(p => ProductMapper.FromDTO(p)).ToList();
        return new CategoryEntity(
            id: categoryDTO.Id,
            name: categoryDTO.Name,
            products: products
            
        );
    }
}