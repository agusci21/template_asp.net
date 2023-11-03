
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
            Email = input.Email.ToLower(),
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

    public Task<SearchUserOutput> SearchUser(SearchUserInput input)
    {
        UserDTO? User;
        if(input.UserId == null && input.Name == null && input.Email == null)
        {
            
            return Task.FromResult(new SearchUserOutput(){
                Error = "invalid_argument"
            });
        }

        User = SearchUserById(input.UserId);
        if(User != null)
        {
            return Task.FromResult(new SearchUserOutput(){
                User = UserMapper.FromDTO(User)
            });
        }
        User = SearchUserByEmail(input.Email);
        if(User != null)
        {
            return Task.FromResult(new SearchUserOutput(){
                User = UserMapper.FromDTO(User)
            });
        }
        User = SearchUserByName(input.Email);
        if(User != null)
        {
            return Task.FromResult(new SearchUserOutput(){
                User = UserMapper.FromDTO(User)
            });
        }

        return Task.FromResult(new SearchUserOutput(){
                Error = "user_not_found"
            }); 
    }

    private UserDTO? SearchUserById(string? userId)
    {
        return DataContext.Users!.Find(userId);
    }
    private UserDTO? SearchUserByName(string? userName)
    {
        if(userName == null)
        {
            return null;
        }
        return DataContext.Users!.FirstOrDefault(p => (p.FirstName != null && p.FirstName.ToLower() == userName.ToLower()) || (p.LastName != null && p.LastName.ToLower() == userName.ToLower()));
    }
    private UserDTO? SearchUserByEmail(string? userEmail)
    {
        if(userEmail == null)
        {
            return null;
        }
        return DataContext.Users!.FirstOrDefault(p => p.Email.ToLower() == userEmail.ToLower());
    }
}