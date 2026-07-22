using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Categories;
using PointOfSale.Application.UseCases.Categories.CreateCategory;

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
}