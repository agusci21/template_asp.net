
namespace Feature.Category;

public static class CategoryModule
{
    public static void RegisterDependencies(IServiceCollection services){
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}