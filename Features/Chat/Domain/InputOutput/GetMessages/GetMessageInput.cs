
namespace Feature.Chat;

public class GetMessagesInput
{
    public required string FirstUserId { get; set; }
    public required string SecondUserId { get; set; }
}