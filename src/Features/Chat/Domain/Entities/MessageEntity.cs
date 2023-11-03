

namespace Feature.Chat;

public class MessageEntity
{
    public required string Id { get; set; }
    public required string OwnerId { get; set; }
    public required string DestinyId { get; set; }
    public required string Text { get; set; }
}