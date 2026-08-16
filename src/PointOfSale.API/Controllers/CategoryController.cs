using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Categories;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.UseCases.Categories.Commands.CreateCategory;
using PointOfSale.Application.UseCases.Categories.Commands.DisableCategory;
using PointOfSale.Application.UseCases.Categories.Commands.UpdateCategory;
using PointOfSale.Application.UseCases.Categories.Queries.GetById;
using PointOfSale.Application.UseCases.Categories.Queries.GetCategoriesList;

namespace PointOfSale.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpPut("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequest request,
        [FromServices] IUpdateCategoryUseCase useCase)
    {
        await useCase.ExecuteAsync(categoryId, request);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get([FromServices] IGetCategoriesListUseCase useCase)
    {
        var categories = await useCase.ExecuteAsync();

        return Ok(categories);
    }

    [HttpGet("{categoryId}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid categoryId, [FromServices] IGetCategoryByIdUseCase useCase)
    {
        var categories = await useCase.ExecuteAsync(categoryId);

        return Ok(categories);
    }

    [HttpDelete("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable([FromRoute] Guid categoryId, [FromServices] IDisableCategoryUseCase useCase)
    {
        await useCase.ExecuteAsync(categoryId);

        return NoContent();
    }
}