using Core.Database;
using DTOs;
using Microsoft.EntityFrameworkCore;
namespace Feature.Product;
public class ProductRepository : IProductRepository
{
    private readonly DataContext DataContext;

    public ProductRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    public Task<GetAllProductOutput> GetAllProducts(string? filterQuery = null, string? categoryId = null)
    {
        var productsDto = DataContext.Products!.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrEmpty(filterQuery))
        {
            productsDto = productsDto.Where(p => p.Name.Contains(filterQuery));
        }

        if (!string.IsNullOrEmpty(categoryId))
        {
            productsDto = productsDto.Where(p => p.CategoryId == categoryId);
        }
        
        var output = new GetAllProductOutput(
            products: productsDto.Select(p => ProductMapper.FromDTO(p))
        );
        return Task.FromResult(output);
    }

    public async Task<GetProductByIdOutput> GetProductById(GetProductByIdInput input)
    {
        var productDTO = await DataContext.Products!.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == input.Id);
        return new GetProductByIdOutput(productDTO);
    }


    public async Task<CreateProductOutput> Save(CreateProductInput input)
    {
        ProductDTO productDTO = new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = input.Name,
            Stock = input.Stock,
            Price = input.Price,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            ImageUrl = input.ImageUrl,
            Category = null,
            CategoryId = null,
        };
        await DataContext.AddAsync(productDTO);
        await DataContext.SaveChangesAsync();
        return new CreateProductOutput(
            productEntity: ProductMapper.FromDTO(productDTO)
        );
    }

    public async Task<UpdateProductOutput> Update(UpdateProductInput input)
    {
        var productToUpdate = DataContext.Products!.Find(input.Id);
        if (productToUpdate == null)
        {
            return new UpdateProductOutput(error: "product_not_found");
        }

        productToUpdate.ModifyWith(
            name: input.Name,
            price: input.Price,
            stock: input.Stock,
            categoryId: input.CategoryId,
            imageUrl: input.ImageUrl
        );

        await DataContext.SaveChangesAsync();
        return new UpdateProductOutput(productEntity: ProductMapper.FromDTO(productToUpdate));
    }

    public async Task<DeleteProductOutput> Delete(DeleteProductInput input)
    {
        var productToDelete = DataContext.Products!.Find(input.Id);
        if (productToDelete == null)
        {
            return new DeleteProductOutput(error: "product_not_found");
        }
        DataContext.Products.Remove(productToDelete);
        await DataContext.SaveChangesAsync();
        return new DeleteProductOutput(product: ProductMapper.FromDTO(productToDelete));
    }

    public async Task<AssociateProductWithCategoryOutput> AssociateProductWithCategory(AssociateProductWithCategoryInput input)
    {
        ProductDTO? ProductDTO = await DataContext.Products!.FindAsync(input.ProductId);
        CategoryDTO? CategoryDTO = await DataContext.Categories!.FindAsync(input.CategoryId);

        if (ProductDTO != null)
        {
            ProductDTO.CategoryId = CategoryDTO?.Id;
            ProductDTO.Category = CategoryDTO;
            CategoryDTO?.Products.Add(ProductDTO);
        }

        await DataContext.SaveChangesAsync();

        return new AssociateProductWithCategoryOutput()
        {
            Product = ProductDTO,
            Category = CategoryDTO,
        };
    }
}