
namespace Feature.Chat;

public class CreateMessageOutput
{
    public MessageEntity? Message { get; set; }
    public string? Error { get; set; }

    public bool IsOk()
    {
        return Message != null && Error == null;
    }
}