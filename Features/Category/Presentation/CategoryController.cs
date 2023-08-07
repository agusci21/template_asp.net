
using Microsoft.AspNetCore.Mvc;

namespace Feature.Category;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository CategoryRepository;
    public CategoryController (ICategoryRepository categoryRepository)
    {
        CategoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var output = await CategoryRepository.GetAllCategories();
        return Ok(new{categories = output.Categories});
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategoryInput input)
    {
        var output = await CategoryRepository.Create(input);

        if(output.Error != null)
        {
            return BadRequest(new {error = output.Error});
        }

        return Ok(new {
            category = output.Category
        });
    }
}