
namespace Feature.Category;

public class GetCategoryByIdInput
{
    public string Id { get; set; }
    public GetCategoryByIdInput(string id)
    {
        Id = id;
    }
}