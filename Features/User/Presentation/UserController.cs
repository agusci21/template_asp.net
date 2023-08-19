
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Feature.User;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository UserRepository;
    public UserController(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserInput input)
    {
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
    public async Task<IActionResult> Login([FromBody] LoginInput input)
    {
        var output = await UserRepository.Login(input);

        if(output.Error != null)
        {
            return NotFound(new {
                output.Error
            });
        }
        output.JWT = JWTHelper.GetSignature();
        return Ok(new {
            user = output.User,
            token = output.JWT
        });
    }
}