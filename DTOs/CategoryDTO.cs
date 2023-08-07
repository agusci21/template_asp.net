using Microsoft.EntityFrameworkCore;

namespace DTOs;
public class CategoryDTO
{
    public required string Id { get; set;}
    public required string Name { get; set;}

    public ICollection<ProductDTO> Products { get; set;} = new List<ProductDTO>();
    public DateTime CreatedAt { get; set;}
    public DateTime UpdatedAt { get; set;}

    public static void OnCategoryModelCreated(ModelBuilder builder)
    {
        var category = builder.Entity<CategoryDTO>();
        category.ToTable("Category");
        category.HasKey(p => p.Id);
        
        category.Property(p => p.Name).IsRequired().HasMaxLength(150);
    }
}