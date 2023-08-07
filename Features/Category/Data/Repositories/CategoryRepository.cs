
namespace Feature.Category;

using System.Collections.ObjectModel;
using Core.Database;
using DTOs;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext DataContext;

    public CategoryRepository (DataContext dataContext)
    {
        DataContext = dataContext;
    }

    public Task<GetAllCategoriesOutput> GetAllCategories()
    {
        var categoriesDTOs = DataContext.Categories;
        var categoriesEntities = categoriesDTOs?.Select(p => CategoryMapper.FromDTO(p));
        var output = new GetAllCategoriesOutput(categories: categoriesEntities);

        return Task.FromResult(output);
    }
    
    public async Task<CreateCategoryOutput> Create(CreateCategoryInput input)
    {
        var existCategoryName =  DataContext.Categories!.Any(p => p.Name == input.Name);
        if(existCategoryName)
        {
            return new CreateCategoryOutput(
                error: "duplicated_category_name"
            );
        }
        var categoryDTO = new CategoryDTO(){
            Id = Guid.NewGuid().ToString(),
            Name = input.Name,
            Products = new Collection<ProductDTO>(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await DataContext.Categories!.AddAsync(categoryDTO);
        await DataContext.SaveChangesAsync();

        var output = new CreateCategoryOutput(
            category: CategoryMapper.FromDTO(categoryDTO)
        );
        return output;
    }

    public async Task<GetCategoryByIdOutput> GetById(GetCategoryByIdInput input)
    {
        var categoryDTO = await DataContext.Categories!.FindAsync(input.Id);
        return new GetCategoryByIdOutput(categoryDTO);
    }
}