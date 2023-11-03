using Feature.Product;
using Feature.Category;
using Feature.User;
using Feature.Chat;

namespace Helpers.ReistorHelper;
public static class RegistorHelper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        CategoryModule.RegisterDependencies(services);
        ProductModule.RegisterDependencies(services);
        UserModule.RegisterDependencies(services);
        ChatModule.RegisterDependencies(services);
    }
}