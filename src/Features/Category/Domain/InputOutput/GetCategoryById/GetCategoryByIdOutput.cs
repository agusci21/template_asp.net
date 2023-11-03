
using DTOs;

namespace Feature.Category;

public class GetCategoryByIdOutput
{
    public CategoryDTO? Category { get; set; }
    public GetCategoryByIdOutput(CategoryDTO? category)
    {
       Category = category;
    }
}