

namespace Feature.Chat;

public struct SendMessageRequestData
{
    public required string To { get; set; }
    public required string Text { get; set; }
}