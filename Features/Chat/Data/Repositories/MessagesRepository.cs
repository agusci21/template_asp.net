
using Core.Database;
using DTOs;
using Microsoft.EntityFrameworkCore.Storage;

namespace Feature.Chat;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext DataContext;

    public MessageRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }
    public Task<GetAllMessagesOutput> GetAllMessages()
    {
        var output = new GetAllMessagesOutput()
        {
            Messages = DataContext.Messages!.Select(m => MessageMapper.FromDTO(m))
        };

        return Task.FromResult(output);
    }

    public async Task<CreateMessageOutput> CreateMessage(CreateMessageInput input)
    {
        var existDestiny = DataContext.Users!.Find(input.DestinyId);
        if (existDestiny == null)
        {
            return new CreateMessageOutput()
            {
                Error = "user_not_found"
            };
        }
        var MessageDTO = new MessageDTO()
        {
            Id = Guid.NewGuid().ToString(),
            DestinyId = input.DestinyId,
            OwnerId = input.OwnerId!,
            Text = input.Text,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await DataContext.Messages!.AddAsync(MessageDTO);
        await DataContext.SaveChangesAsync();
        return new CreateMessageOutput()
        {
            Message = MessageMapper.FromDTO(MessageDTO)
        };
    }

    Task<GetMessagesOutput> IMessageRepository.GetMessages(GetMessagesInput input)
    {
        var messagesDTO = DataContext.Messages?
                .Where(m => (m.OwnerId == input.FirstUserId && m.DestinyId == input.SecondUserId) || (m.OwnerId == input.SecondUserId && m.DestinyId == input.FirstUserId));
        var output = new GetMessagesOutput()
        {
            Messages = messagesDTO!.Select(m => MessageMapper.FromDTO(m)).ToList<MessageEntity>()
        };
        return Task.FromResult(output);
    }
}