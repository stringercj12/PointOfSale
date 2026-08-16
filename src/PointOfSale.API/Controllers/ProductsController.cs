using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.UseCases.Products.Commands.CreateProduct;
using PointOfSale.Application.UseCases.Products.Commands.DeleteProduct;
using PointOfSale.Application.UseCases.Products.Commands.UpdateProductBasicInfo;
using PointOfSale.Application.UseCases.Products.Commands.UpdateProductPhotos;
using PointOfSale.Application.UseCases.Products.Queries.GetProductById;
using PointOfSale.Application.UseCases.Products.Queries.GetProductsByCategory;
using PointOfSale.Application.UseCases.Products.Queries.GetProductsList;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetProductsListRequest request,
            [FromServices] IGetProductsListUseCase useCase)
        {
            var products = await useCase.ExecuteAsync(request);

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

        [HttpGet("{sku}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductBySKU([FromServices] IGetProductBySkuUseCase useCase,
            [FromRoute] string sku)
        {
            var product = await useCase.ExecuteAsync(sku);

            return Ok(product);
        }

        [HttpGet("category/{productId}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductsByCategory([FromServices] IGetProductsByCategoryUseCase useCase,
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
            [FromForm] List<ProductPhotoListRequest> files, [FromServices] IUpdateProductPhotosUseCase useCase)
        {

            var request = new UpdateProductPhotosRequest(productId, files);

            await useCase.ExecuteAsync(request);

            return NoContent();
        }

        [HttpPut("{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid productId, [FromForm] UpdateProductBasicInfoRequest request, [FromServices] IUpdateProductBasicInfoUseCase useCase)
        {

            await useCase.ExecuteAsync(productId, request);

            return NoContent();
        }

        [HttpDelete("{productId:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid productId, [FromServices] IDisableProductUseCase useCase)
        {
            await useCase.ExecuteAsync(productId);

            return NoContent();
        }
    }
}