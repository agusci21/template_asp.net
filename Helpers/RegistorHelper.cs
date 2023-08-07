using Feature.Product;
using Feature.Category;
namespace Helpers.ReistorHelper;
public static class RegistorHelper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        CategoryModule.RegisterDependencies(services);
        ProductModule.RegisterDependencies(services);
    }
}