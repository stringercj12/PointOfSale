using PointOfSale.Application.DTOs.Response.Tables;

namespace PointOfSale.Application.UseCases.Tables.Queries.GetListOfTables
{
    public interface IGetListOfTablesUseCase
    {
        Task<IEnumerable<TablesResponse>> ExecuteAsync();
    }
}
