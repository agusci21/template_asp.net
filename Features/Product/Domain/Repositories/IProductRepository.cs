namespace Feature.Product;
using DTOs;
public interface IProductRepository
{
    Task<GetAllProductOutput> GetAllProducts();
    Task<CreateProductOutput> Save(CreateProductInput productDTO);

    Task<UpdateProductOutput> Update(UpdateProductInput input);
    Task<GetProductByIdOutput> GetProductById (GetProductByIdInput input);

    Task<DeleteProductOutput> Delete(DeleteProductInput input);

    Task<AssociateProductWithCategoryOutput> AssociateProductWithCategory(AssociateProductWithCategoryInput input); 
}