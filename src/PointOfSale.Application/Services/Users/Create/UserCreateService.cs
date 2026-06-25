using FluentValidation;
using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.DTOs.Response.Users;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Services.Common.Interfaces;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Application.Services.Users.Create
{
    public class UserCreateService : IUserCreateService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserCreateRequest> _validator;
        private readonly IStorageService _storageService;
        private readonly ICryptoService _cryptoService;

        public UserCreateService(IUserRepository repository, IUnitOfWork unitOfWork, IValidator<UserCreateRequest> validator, ICryptoService cryptoService, IStorageService storageService)
        {
            _userRepository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _cryptoService = cryptoService;
            _storageService = storageService;
        }

        public async Task<UserCreateResponse> ExecuteAsync(UserCreateRequest request)
        {
            _validator.ValidateAndThrowCustom(request);

            var exists = await _userRepository.EmailOrUsernameExistsAsync(request.Email, request.Username);
            if (exists)
                throw new DuplicateResourceException("O e-mail ou nome de usuário informado já está em uso.");

            var passwordHash = _cryptoService.HashPassword(request.Password);


            Stream? imageStream = null;
            string? fileName = null;
            string profileImageUrl = string.Empty;

            if (request.ProfileImage is not null)
            {
                imageStream = request.ProfileImage.OpenReadStream();
                fileName = request.ProfileImage.FileName;
            }


            if (imageStream is not null && !string.IsNullOrWhiteSpace(fileName))
            {
                profileImageUrl = await _storageService.UploadAsync(imageStream, fileName);
            }

            var newUser = User.Create(
                 request.Name,
                 request.Username,
                 request.Email,
                 profileImageUrl,
                 passwordHash,
                 request.Role
             );

            await _userRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return new UserCreateResponse(newUser.Id, newUser.Email);
        }

    }
}
