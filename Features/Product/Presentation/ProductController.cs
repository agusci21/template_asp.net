using Microsoft.AspNetCore.Mvc;

namespace Feature.Product;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository ProductRepository;

    public ProductController(IProductRepository productRepository)
    {
        ProductRepository = productRepository;
    }

    [HttpGet]
    public IActionResult Get([FromQuery(Name = "filterQuery")] string? filterQuery, [FromQuery(Name = "categoryId")] string? categoryId)
    {
        var json = new
        {
            products = ProductRepository.GetAllProducts(filterQuery, categoryId).Result.Products
        };
        return Ok(json);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProductByIdInput([FromRoute] string id)
    {
        var input = new GetProductByIdInput(id);
        var output = await ProductRepository.GetProductById(input);

        if (output.Product == null)
        {
            return NotFound(new
            {
                error = "product_not_found"
            });
        }
        var product = ProductMapper.FromDTO(output.Product);
        return Ok(new { product });
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductInput input)
    {

        ProductEntity product = new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = input.Name,
            Stock = input.Stock,
            Price = input.Price,
            ImageUrl = input.ImageUrl
        }

        ;

        var output = await ProductRepository.GetAllProducts();
        bool existProduct = output.Products.Any(x => x.Name == product.Name);

        if (existProduct)
        {
            var json = new
            {
                error = "duplicated product name",
            };
            return BadRequest(json);
        }

        var createdProduct = await ProductRepository.Save(input);
        if (createdProduct == null)
        {
            var json = new
            {
                error = "Could not create a new product",
            };
            return StatusCode(500, json);
        }
        return Ok(new
        {
            message = "product_created",
            product
        });
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] string Id, [FromBody] UpdateProductInput input)
    {
        input.SetId(Id);
        var output = await ProductRepository.Update(input);

        if (output.ProductEntity == null)
        {
            return NotFound(new
            {
                output.Error
            });
        }
        return Ok(new { product = output.ProductEntity });
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var input = new DeleteProductInput(id);
        var output = await ProductRepository.Delete(input);

        if (output.Product == null)
        {
            return NotFound(new { output.Error });
        }

        return Ok(new { product_deleted = output.Product });
    }
    [HttpPost]
    [Route("associate")]
    public async Task<IActionResult> AssociateProductWithCategory([FromBody] AssociateProductWithCategoryInput input)
    {
        var output = await ProductRepository.AssociateProductWithCategory(input);

        if (output.Product == null && output.Category == null)
        {
            return BadRequest(new
            {
                message = "not_product_and_category_founded",
            });
        }
        if (output.Product == null)
        {
            return BadRequest(new
            {
                message = "not_product_founded",
            });
        }
        if (output.Category == null)
        {
            return BadRequest(new
            {
                message = "not_cateory_founded",
            });
        }
        return Ok(new
        {
            meesage = "assosiation_completed",
            product = ProductMapper.FromDTO(output.Product),
            category = CategoryMapper.FromDTO(output.Category)
        });
    }
}

