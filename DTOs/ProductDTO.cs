using Microsoft.EntityFrameworkCore;

namespace DTOs;
public class ProductDTO
{

    public required string Id { get; set; }
    public required string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public string? CategoryId { get; set; }
    public CategoryDTO? Category { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public static void OnProductModelCreated(ModelBuilder builder)
    {
        var product = builder.Entity<ProductDTO>();
        product.ToTable("Product");
        product.HasKey(p => p.Id);

        product.Property(p => p.Name).IsRequired().HasMaxLength(150);
        product.Property(p => p.Price).IsRequired();
        product.Property(p => p.Stock).IsRequired();

        product.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);
    }

    public void ModifyWith(string? name, int? stock, double? price, string? categoryId, string? imageUrl)
    {
        this.Name = name ?? this.Name;
        this.Stock = stock ?? this.Stock;
        this.Price = price ?? this.Price;
        this.CategoryId = categoryId ?? this.CategoryId;
        this.ImageUrl = imageUrl ?? this.ImageUrl;
        this.UpdatedAt = DateTime.Now;
    }

}