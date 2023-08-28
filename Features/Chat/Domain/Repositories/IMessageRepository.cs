

namespace Feature.Chat;

public interface IMessageRepository
{
    public Task<GetAllMessagesOutput> GetAllMessages();
}