namespace Feature.Product;
public interface IProductRepository
{
    Task<GetAllProductOutput> GetAllProducts(string? filterQuery = null, string? categoryId = null);
    Task<CreateProductOutput> Save(CreateProductInput productDTO);

    Task<UpdateProductOutput> Update(UpdateProductInput input);
    Task<GetProductByIdOutput> GetProductById (GetProductByIdInput input);

    Task<DeleteProductOutput> Delete(DeleteProductInput input);

    Task<AssociateProductWithCategoryOutput> AssociateProductWithCategory(AssociateProductWithCategoryInput input); 
}