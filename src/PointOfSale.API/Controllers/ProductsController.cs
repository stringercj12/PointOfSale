using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Products.Commands.CreateProduct;
using PointOfSale.Application.UseCases.Products.Commands.UpdateProductPhotos;
using PointOfSale.Application.UseCases.Products.Queries.GetProductById;
using PointOfSale.Application.UseCases.Products.Queries.GetProductsList;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] GetProductsListRequest request,
            [FromServices] IGetProductsListUseCase useCase)
        {
            var products = await useCase.ExecuteAsync(request);

            if (products.Count == 0)
                return NoContent();

            return Ok(products);
        }


        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProduct([FromServices] IGetProductByIdUseCase useCase,
            [FromRoute] Guid productId)
        {
            var product = await useCase.ExecuteAsync(productId);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request,
            [FromServices] ICreateProductUseCase useCase)
        {
            await useCase.ExecuteAsync(request);

            return Created();
        }

        [HttpPut("{productId:guid}/photos")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePhotos(
            [FromRoute] Guid productId,
            [FromForm] List<IFormFile> files, [FromServices] IUpdateProductPhotosUseCase useCase)
        {
            
            var request = new UpdateProductPhotosRequest(productId, files);

            await useCase.ExecuteAsync(request);

            return NoContent();
        }
    }
}