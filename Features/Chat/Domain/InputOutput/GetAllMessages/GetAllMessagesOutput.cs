namespace Feature.Chat;

public class GetAllMessagesOutput
{
    public required IEnumerable<MessageEntity> Messages { get; set; }
}