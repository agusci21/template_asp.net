
using Core.Database;
using DTOs;
using Helpers;

namespace Feature.User;

public class UserRepository : IUserRepository
{
    private readonly DataContext DataContext;

    public UserRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }
    public async Task<CreateUserOutput> CreateUser(CreateUserInput input)
    {
        UserDTO userDTO = new(){
            Id = Guid.NewGuid().ToString(),
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,
            Password = EncriptPasswordHelper.Encript(input.Password),
            PhoneNumber = input.PhoneNumber,
            PersonalIdentifier = input.PersonalIdentifier,
            Birthdate = input.Birthdate,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await DataContext.Users!.AddAsync(userDTO);
        await DataContext.SaveChangesAsync();
        return new CreateUserOutput{
            UserDTO = userDTO
        };
    }

    public Task<LoginOutput> Login(LoginInput input)
    {
        var user = DataContext.Users!.FirstOrDefault(p => p.Email.ToLower() == input.Email.ToLower());
        if(user == null)
        {
            return Task.FromResult(new LoginOutput{
                Error = "user_not_found"
            }); 
        }

        if(EncriptPasswordHelper.Encript(input.Password) != user.Password)
        {
            return Task.FromResult(
                new LoginOutput{
                    Error = "invalid_credentials"
                }
            );
        }
        return Task.FromResult(new LoginOutput{
            User = UserMapper.FromDTO(user),
        });
    }
}