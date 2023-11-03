using DTOs;

namespace Feature.Chat;

public static class MessageMapper
{
    public static MessageEntity FromDTO(MessageDTO messageDTO)
    {
        return new MessageEntity()
        {
            Id = messageDTO.Id,
            Text = messageDTO.Text,
            OwnerId = messageDTO.OwnerId,
            DestinyId = messageDTO.DestinyId,
        };
    }
}