
namespace Feature.User;

public class LoginOutput 
{
    public UserEntity? User {get; set;}
    public string? JWT {get; set;}
    public string? Error {get; set;}
}