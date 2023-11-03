
using Microsoft.EntityFrameworkCore;

namespace DTOs;

public class MessageDTO
{
    public required string Id { get; set; }
    public required string Text { get; set; }
    public required string OwnerId { get; set; }
    public UserDTO? Owner { get; set; }
    public required string DestinyId { get; set; }
    public UserDTO? Destiny { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public static void OnMessageModelCreated(ModelBuilder builder)
    {
        var message = builder.Entity<MessageDTO>();
        message.ToTable("Message");
        message.HasKey(p => p.Id);

        message.Property(p => p.Text).IsRequired();
        message.HasOne(p => p.Owner).WithMany().HasForeignKey(p => p.OwnerId);
        message.HasOne(p => p.Destiny).WithMany().HasForeignKey(p => p.DestinyId);
    }
}