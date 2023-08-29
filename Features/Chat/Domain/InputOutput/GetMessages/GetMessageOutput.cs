
namespace Feature.Chat;

public class GetMessagesOutput
{
    public required IEnumerable<MessageEntity> Messages { get; set; }
}