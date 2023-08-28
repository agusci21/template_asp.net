
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feature.Chat;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMessageRepository MessageRepository;
    public ChatController(IMessageRepository messageRepository)
    {
        MessageRepository = messageRepository;
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
    public async Task<IActionResult> CreateMessage([FromHeader] string token )
    {
        return Ok(token);
    }
}   