using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Categories;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.UseCases.Categories.Commands.CreateCategory;
using PointOfSale.Application.UseCases.Categories.Queries.GetCategoriesList;
using PointOfSale.Application.UseCases.Products.Queries.GetProductsList;

namespace PointOfSale.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request,
        [FromServices] ICreateCategoryUseCase useCase)
    {
        await useCase.ExecuteAsync(request);

        return Created();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get([FromServices] IGetCategoriesListUseCase useCase)
    {
        var products = await useCase.ExecuteAsync();

        if (products.Count == 0)
            return NoContent();

        return Ok(products);
    }
}