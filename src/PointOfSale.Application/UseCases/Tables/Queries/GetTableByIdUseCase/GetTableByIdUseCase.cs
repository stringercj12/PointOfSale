using PointOfSale.Application.DTOs.Response.Tables;
using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Tables.Queries.GetTableByIdUseCase
{
    public class GetTableByIdUseCase : IGetTableByIdUseCase
    {
        private readonly ITablesRepository _tableRepository;

        public GetTableByIdUseCase(ITablesRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<TablesResponse> ExecuteAsync(Guid tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId) ?? throw new NotFoundException($"A mesaa com ID '{tableId}' não foi encontrada.");


            return new TablesResponse(
                table.Id,
                table.Name,
                table.Number,
                table.Status,
                table.Capacity,
                table.CreatedAt
            );
        }
    }
}
