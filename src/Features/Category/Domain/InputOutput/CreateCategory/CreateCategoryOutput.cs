
namespace Feature.Category;

public class CreateCategoryOutput
{
    public CategoryEntity? Category {get;}
    public string? Error {get;}

    public CreateCategoryOutput(CategoryEntity? category)
    {
        Category = category;
    }
    public CreateCategoryOutput(string? error)
    {
        Error = error;
    }

}