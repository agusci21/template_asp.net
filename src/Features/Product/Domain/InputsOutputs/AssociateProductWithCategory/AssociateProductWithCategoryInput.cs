
namespace Feature.Product;

public class AssociateProductWithCategoryInput
{
    public string ProductId { get; set;}
    public string CategoryId { get; set;}

    public AssociateProductWithCategoryInput(string productId, string categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

}