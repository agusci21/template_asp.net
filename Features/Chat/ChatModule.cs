namespace Feature.Chat;

public static class ChatModule
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
    }
}