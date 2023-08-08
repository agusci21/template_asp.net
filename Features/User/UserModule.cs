
namespace Feature.User;

public static class UserModule
{
    public static void RegisterDependencies(IServiceCollection services){
        services.AddScoped<IUserRepository, UserRepository>();
    }
}