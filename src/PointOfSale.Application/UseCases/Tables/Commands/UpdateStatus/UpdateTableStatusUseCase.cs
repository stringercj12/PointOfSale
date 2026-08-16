using PointOfSale.Domain.Enums;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Tables.Commands.UpdateStatus
{
    public class UpdateTableStatusUseCase : IUpdateTableStatusUseCase
    {
        private readonly ITablesRepository _tableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTableStatusUseCase(ITablesRepository tableRepository, IUnitOfWork unitOfWork)
        {
            _tableRepository = tableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid tableId, TableStatus newStatus)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);

            if (table is null)
                return false;

            switch (newStatus)
            {
                case TableStatus.Occupied:
                    table.Occupy();
                    break;
                case TableStatus.Reserved:
                    table.Reserve();
                    break;
                case TableStatus.Free:
                    table.Release();
                    break;
                case TableStatus.Unavailable:
                    table.MakeUnavailable();
                    break;
                case TableStatus.Closed:
                    table.Close();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), $"Status {newStatus} não é suportado.");
            }

            _tableRepository.Update(table);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
