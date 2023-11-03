

namespace Feature.Chat;

public interface ISendMessageUseCase
{
    public Task<SendMessageOutput> Excecute(SendMessageInput input);
}