using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Validations;
using PointOfSale.Communication.Request.User;
using PointOfSale.Communication.Response;
using PointOfSale.Communication.Response.User;
using PointOfSale.Domain.Entities;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Application.Users.Create
{
    public class UserCreateService : IUserCreateService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreateService(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserCreateResponse> CreateUserAsync(UserCreateRequest request)
        {
            ValidateRequest(request);

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


        private void ValidateRequest(UserCreateRequest request)
        {

            var validator = new UserCreateRequestValidator();
            var resultValidator = validator.Validate(request);

            if (resultValidator.IsValid == false)
            {
                var result = resultValidator.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(result);
            }

        }
    }
}
