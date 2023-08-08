
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
        if(output.UserDTO == null)
        {
            return BadRequest(new {
                message = "could_not_create_user"
            });
        }
        return Ok(new {
            User = output.UserDTO
        });
    }
}