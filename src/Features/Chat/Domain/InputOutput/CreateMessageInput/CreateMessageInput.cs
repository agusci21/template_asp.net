namespace Feature.Chat;

public class CreateMessageInput
{
    public string? OwnerId { get; set; }
    public required string DestinyId { get; set; }
    public required string Text { get; set; }

    public CreateMessageInput(string destinyId, string text)
    {
        DestinyId = destinyId;
        Text = text;
    }
}