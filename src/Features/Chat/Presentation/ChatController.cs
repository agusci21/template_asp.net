
using System.Net.WebSockets;
using System.Text;
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
    [Route("{to}")]
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

    [Route("websocket")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task WebSocket([FromHeader(Name = "x-token")] string token)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var userId = await TokenRepository.GetUserIdFromToken(token);
            if (userId == null)
            {
                return;
            }
            await HandleWebSocket(userId, webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }
    private static async Task HandleWebSocket(string userId, WebSocket webSocket)
    {
        var buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}