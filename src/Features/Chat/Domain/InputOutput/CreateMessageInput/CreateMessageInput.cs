namespace Feature.Chat;

public struct CreateMessageInput
{
    public string? OwnerId;
    public required string DestinyId;
    public required string Text;

}