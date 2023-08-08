
namespace Feature.User;

public struct CreateUserInput
{
    public string FirstName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PersonalIdentifier { get; set; }
    public DateTime? Birthdate { get; set; }
}