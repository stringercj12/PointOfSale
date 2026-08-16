using FluentValidation;
using PointOfSale.Application.DTOs.Request.Tables;
using PointOfSale.Application.DTOs.Response.Tables;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Tables.Commands
{
    public class CreateTableUseCase : ICreateTableUseCase
    {
        private readonly ITablesRepository _tableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateTableRequest> _validator;

        public CreateTableUseCase(
            IUnitOfWork unitOfWork,
            IValidator<CreateTableRequest> validator,
            ITablesRepository tableRepository)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _tableRepository = tableRepository;
        }
        public async Task ExecuteAsync(CreateTableRequest request)
        {
            _validator.ValidateAndThrowCustom(request);

            var existsWithName = await
                _tableRepository.ExistsByNameAsync(request.Name);
            if (existsWithName) throw new BusinessException($"Já existe uma mesa cadastrada com esse nome '{request.Name}'.");

            var newTable = Table.Create(request.Name, request.Number,  request.Description, request.Capacity);


            _tableRepository.Add(newTable);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
