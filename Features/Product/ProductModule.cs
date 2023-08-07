namespace Feature.Product;
static class ProductModule
{
    public static void RegisterDependencies(IServiceCollection services){
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}