using PointOfSale.Application.DTOs.Response.Tables;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Tables.Queries.GetListOfTables
{
    public class GetListOfTablesUseCase : IGetListOfTablesUseCase
    {
        private readonly ITablesRepository _tablesRepository;
        public GetListOfTablesUseCase(ITablesRepository tablesRepository)
        {
            _tablesRepository = tablesRepository;
        }
        public async Task<IEnumerable<TablesResponse>> ExecuteAsync()
        {
            var tables = await _tablesRepository.GetAllTablesAsync();

            return tables.Select(t => new TablesResponse(
                t.Id, 
                t.Name, 
                t.Number, 
                t.Status, 
                t.Capacity, 
                t.CreatedAt
            )).ToList();
        }
    }
}
