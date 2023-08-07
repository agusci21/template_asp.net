
namespace Feature.Category;

public interface ICategoryRepository
{
    Task<CreateCategoryOutput> Create (CreateCategoryInput input);
    Task<GetCategoryByIdOutput> GetById(GetCategoryByIdInput input);
    Task<GetAllCategoriesOutput> GetAllCategories();
}