
namespace Feature.Category;

public class GetAllCategoriesOutput
{
    public string? Error {get;}
    public IEnumerable<CategoryEntity>? Categories {get;}

    public GetAllCategoriesOutput(string? error)
    {
        Error = error;
    }

    public GetAllCategoriesOutput(IEnumerable<CategoryEntity>? categories)
    {
        Categories = categories;
    }
}