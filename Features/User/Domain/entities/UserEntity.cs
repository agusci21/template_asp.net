
namespace Feature.User;

public class UserEntity
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string? PhoneNumber { get; set; }
    public string? PersonalIdentifier { get; set; }
    public DateTime? Birthdate { get; set; }

}