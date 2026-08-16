
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Tables;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.DTOs.Response.Tables;
using PointOfSale.Application.UseCases.Tables.Commands;
using PointOfSale.Application.UseCases.Tables.Commands.UpdateStatus;
using PointOfSale.Application.UseCases.Tables.Queries.GetListOfTables;
using PointOfSale.Application.UseCases.Tables.Queries.GetTableByIdUseCase;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Enums;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointOfSale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TablesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IGetListOfTablesUseCase useCase)
        {
            var tables = await useCase.ExecuteAsync();

            return Ok(tables);
        }

        [HttpGet("{tableId}")]
        [ProducesResponseType(typeof(TablesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid tableId, [FromServices] IGetTableByIdUseCase useCase)
        {
            var table = await useCase.ExecuteAsync(tableId);

            return Ok(table);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TablesResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateTableRequest request, [FromServices] ICreateTableUseCase useCase)
        {
            await useCase.ExecuteAsync(request);

            return Created();
        }


        [HttpPut("{tableId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] Guid tableId, [FromBody] UpdateTableRequest request, [FromServices] ICreateTableUseCase useCase)
        {
            //await useCase.ExecuteAsync(tableId, request);

            return NoContent();
        }


        [HttpPatch("{tableId:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid tableId, [FromBody] UpdateTableStatusRequest request, [FromServices] IUpdateTableStatusUseCase useCase)
        {
            await useCase.ExecuteAsync(tableId, request.Status);

            return NoContent();
        }

        [HttpDelete("{tableId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid tableId, [FromServices] ICreateTableUseCase useCase)
        {

            //await useCase.ExecuteAsync(tableId);

            return NoContent();
        }
    }
}
