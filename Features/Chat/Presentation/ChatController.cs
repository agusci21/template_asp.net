
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
        var output = await MessageRepository.CreateMessage(input);
        if (output.IsOk())
        {
            return Ok(new
            {
                message = output.Message,
            });
        }
        return BadRequest(new
        {
            error = output.Error
        });
    }
    [HttpGet]
    [Route("{to}/")]
    public async Task<IActionResult> GetMessages([FromHeader(Name = "x-token")] string token, [FromRoute(Name = "to")] string ToId)
    {
        var FirstUserId = await TokenRepository.GetUserIdFromToken(token);
        var input = new GetMessagesInput()
        {
            SecondUserId = ToId,
            FirstUserId = FirstUserId!
        };
        var output = await MessageRepository.GetMessages(input);

        return Ok(new
        {
            messages = output.Messages
        });
    }
}