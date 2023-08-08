
namespace Feature.User;

public interface IUserRepository
{
    public Task<CreateUserOutput> CreateUser(CreateUserInput input);
}