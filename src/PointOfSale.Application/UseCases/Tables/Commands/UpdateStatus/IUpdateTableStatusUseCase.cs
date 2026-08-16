

using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.UseCases.Tables.Commands.UpdateStatus
{
   public interface IUpdateTableStatusUseCase
    {
        Task<bool> ExecuteAsync(Guid tableId, TableStatus status);
    }
}
