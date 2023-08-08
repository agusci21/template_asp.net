
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
}