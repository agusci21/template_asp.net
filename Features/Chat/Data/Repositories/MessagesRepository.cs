
using Core.Database;

namespace Feature.Chat;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext DataContext;

    public MessageRepository (DataContext dataContext)
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
}