namespace Feature.Product;

public class UpdateProductOutput
{
    public ProductEntity? ProductEntity { get; set; }
    public string? Error { get; set; }
    public UpdateProductOutput(ProductEntity productEntity){
        ProductEntity = productEntity;
    }
    public UpdateProductOutput(string error){
        Error = error;
    }
}