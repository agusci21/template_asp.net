
using Microsoft.AspNetCore.Mvc;

namespace Feature.Chat;


[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{

    private readonly ISendMessageUseCase UseCase;

    public ChatController(ISendMessageUseCase useCase)
    {
        UseCase = useCase;
    }

    [HttpPost]
    [Route("send")]
    public async Task<IActionResult> SendMessage([FromHeader(Name = "x-token")] string fromToken, [FromBody] SendMessageRequestData body)
    {
        SendMessageInput input = new()
        {
            FromToken = fromToken,
            To = body.To,
            Text = body.Text,
        };

        SendMessageOutput output = await UseCase.Excecute(input);

        if (output.Error != null)
        {
            return BadRequest(new
            {
                error = output.Error
            });
        }
        return Ok(new
        {
            output.Message
        });
    }
}