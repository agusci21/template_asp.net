

namespace Feature.Chat;

public interface IMessageRepository
{
    public Task<GetAllMessagesOutput> GetAllMessages();
    public Task<CreateMessageOutput> CreateMessage(CreateMessageInput input);
}