using FluentValidation;
using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.DTOs.Response.Users;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Application.Services.Users.Create
{
    public class UserCreateService : IUserCreateService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserCreateRequest> _validator;

        public UserCreateService(IUserRepository repository, IUnitOfWork unitOfWork, IValidator<UserCreateRequest> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<UserCreateResponse> CreateUserAsync(UserCreateRequest request)
        {
            _validator.ValidateAndThrowCustom(request);

            var exists = await _repository.EmailOrUsernameExistsAsync(request.Email, request.Username);
            if (exists)
                throw new DuplicateResourceException("O e-mail ou nome de usuário informado já está em uso.");


            string imagePath = string.Empty;
            if (request.ProfileImage is { Length: > 0 })
            {
                //imagePath = await _storageService.UploadAsync(request.ProfileImage);
            }

            var user = new User(
                request.Name,
                request.Username,
                request.Email,
                request.Password, 
                request.Role,
                request.Active,
                imagePath
            );


            await _repository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new UserCreateResponse(
                 user.Id,
                 user.Email
             );
        }
    }
}
