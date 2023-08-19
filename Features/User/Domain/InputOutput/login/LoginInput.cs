namespace Feature.User;

public class LoginInput
{
    public string Email {get; set;}
    public string Password { get; set;}

    public LoginInput(string email, string password)
    {
        Email = email;
        Password = password;
    }
}