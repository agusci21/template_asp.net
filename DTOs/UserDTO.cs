using Microsoft.EntityFrameworkCore;

namespace DTOs;
public class UserDTO
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PersonalIdentifier { get; set; }
    public DateTime? Birthdate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static void OnUserModelCreated(ModelBuilder builder)
    {
        var user = builder.Entity<UserDTO>();
        user.ToTable("User");
        user.HasKey(p => p.Id);

        user.Property(p => p.FirstName).IsRequired().HasMaxLength(150);
        user.Property(p => p.Email).IsRequired();
        user.Property(p => p.Password).IsRequired();
        user.Property(p => p.CreatedAt).IsRequired();
        user.Property(p => p.UpdatedAt).IsRequired();

    }

}