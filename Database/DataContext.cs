namespace Core.Database;

using Microsoft.EntityFrameworkCore;
using DTOs;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Configuration.GetConnectionString("TemplateConectionString"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       ProductDTO.OnProductModelCreated(builder);
       CategoryDTO.OnCategoryModelCreated(builder);
       UserDTO.OnUserModelCreated(builder);
    }

    public DbSet<ProductDTO>? Products { get; set; }
    public DbSet<CategoryDTO>? Categories { get; set; }
    public DbSet<UserDTO>? Users { get; set; }

}