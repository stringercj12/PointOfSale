using PointOfSale.Application.DTOs.Request.Tables;

namespace PointOfSale.Application.UseCases.Tables.Commands
{
    public interface ICreateTableUseCase
    {
        Task ExecuteAsync(CreateTableRequest request);
    }
}
