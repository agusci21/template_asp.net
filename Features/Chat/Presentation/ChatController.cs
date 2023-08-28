
using Feature.User;
using Microsoft.AspNetCore.Mvc;

namespace Feature.Chat;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMessageRepository MessageRepository;
    private readonly ITokenRepository TokenRepository;
    public ChatController(IMessageRepository messageRepository, ITokenRepository tokenRepository)
    {
        MessageRepository = messageRepository;
        TokenRepository = tokenRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var output = await MessageRepository.GetAllMessages();
        return Ok(new
        {
            messages = output.Messages
        });
    }
    [HttpPost]
    [Route("send-message")]
    public async Task<IActionResult> CreateMessage([FromHeader(Name = "x-token")] string token, [FromBody] CreateMessageInput messageInput)
    {
        var userId = await TokenRepository.GetUserIdFromToken(token);
        var input = messageInput;
        input.OwnerId = userId;
        return Ok(input);
    }
}