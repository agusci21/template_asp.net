
namespace Feature.User;

public interface IUserRepository
{
    public Task<CreateUserOutput> CreateUser(CreateUserInput input);
    public Task<LoginOutput> Login (LoginInput input);
}