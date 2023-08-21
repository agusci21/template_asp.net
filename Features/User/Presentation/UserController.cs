
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feature.User;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository UserRepository;
    private readonly ITokenRepository TokenRepository;
    public UserController(IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        UserRepository = userRepository;
        TokenRepository = tokenRepository;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateUserInput input)
    {   
        var searchUserInput = new SearchUserInput (){
            Email = input.Email.ToLower(),
        };

        var previousUserOutput = await UserRepository.SearchUser(searchUserInput);

        if(previousUserOutput.User != null)
        {
            return BadRequest(new {
                error = "duplicated_email"
            });
        }
        var output = await UserRepository.CreateUser(input);
        if (output.UserDTO == null)
        {
            return BadRequest(new
            {
                message = "could_not_create_user"
            });
        }

        UserEntity user = new()
        {
            Id = output.UserDTO.Id,
            FirstName = output.UserDTO.FirstName,
            LastName = output.UserDTO.LastName,
            Email = output.UserDTO.Email,
            PhoneNumber = output.UserDTO.PhoneNumber,
            PersonalIdentifier = output.UserDTO.PersonalIdentifier,
            Birthdate = output.UserDTO.Birthdate,
        };
        return Ok(new
        {
            user
        });
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginInput input)
    {
        var output = await UserRepository.Login(input);

        if(output.Error != null || output.User == null)
        {
            var errorMessage = new {
                error = output.Error
            };
            return output.Error == "user_not_found" ? NotFound(errorMessage) : BadRequest(errorMessage);
        }

        var token = await TokenRepository.GetTokenAsync(output.User.Id);
        
        return Ok(new {
            user = output.User,
            token
        });
    }
}
