namespace Feature.User;

public interface ITokenRepository 
{
    Task<string> GetTokenAsync(string UserId);
}