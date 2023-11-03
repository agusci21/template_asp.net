

using System.Collections;
using Feature.User;

namespace Feature.Chat;

class SendMessageUseCase : ISendMessageUseCase
{
    private readonly IUserRepository UserRepository;
    private readonly IMessageRepository MessageRepository;
    private readonly ITokenRepository TokenRepository;

    public SendMessageUseCase(IUserRepository userRepository, IMessageRepository messageRepository, ITokenRepository tokenRepository)
    {
        UserRepository = userRepository;
        MessageRepository = messageRepository;
        TokenRepository = tokenRepository;
    }
    public async Task<SendMessageOutput> Excecute(SendMessageInput input)
    {
        ArrayList Errors = new() { };

        string? senderId = await TokenRepository.GetUserIdFromToken(input.FromToken); 

        SearchUserInput FromUserInput = new()
        {
            UserId = senderId
        };
        SearchUserInput ToUserInput = new()
        {
            UserId = input.To
        };

        var FromUserOutput = await UserRepository.SearchUser(FromUserInput);
        var ToUserOutput = await UserRepository.SearchUser(ToUserInput);

        if (FromUserOutput.Error != null)
        {
            Errors.Add(FromUserOutput.Error);
        }

        if (ToUserOutput.Error != null)
        {
            Errors.Add(ToUserOutput.Error);
        }

        if (Errors.Count > 0)
        {
            return new SendMessageOutput()
            {
                Error = string.Join("_and_", Errors.ToArray())
            };
        }

        CreateMessageInput createMessageInput = new()
        {
            DestinyId = input.To,
            OwnerId = input.FromToken,
            Text = input.Text,
        };
        CreateMessageOutput createMessageOutput = await MessageRepository.CreateMessage(createMessageInput);

        return new SendMessageOutput()
        {
            Error = createMessageOutput.Error,
            Message = createMessageOutput.Message
        };
    }
}