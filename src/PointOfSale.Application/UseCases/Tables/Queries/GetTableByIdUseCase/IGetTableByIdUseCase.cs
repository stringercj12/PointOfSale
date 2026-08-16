using PointOfSale.Application.DTOs.Response.Tables;

namespace PointOfSale.Application.UseCases.Tables.Queries.GetTableByIdUseCase
{
    public interface IGetTableByIdUseCase
    {
        Task<TablesResponse> ExecuteAsync(Guid tableId);
    }
}
