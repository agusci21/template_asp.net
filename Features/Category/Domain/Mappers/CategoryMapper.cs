namespace Feature.Category;
using DTOs;
public static class CategoryMapper
{
    public static CategoryEntity FromDTO (CategoryDTO categoryDTO)
    {
        return new CategoryEntity(
            id: categoryDTO.Id,
            name: categoryDTO.Name
        );
    }
}