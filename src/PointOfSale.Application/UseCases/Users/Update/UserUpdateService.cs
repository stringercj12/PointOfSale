using FluentValidation;
using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Services.Users.Update;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Users.Update;

public class UserUpdateService : IUserUpdateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserUpdateRequest> _validator;

    public UserUpdateService(IUserRepository userRepository, IUnitOfWork unitOfWork,
        IValidator<UserUpdateRequest> validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task ExecuteAsync(UserUpdateRequest request, int userId)
    {
        await _validator.ValidateAndThrowAsync(request);

        var user = await _userRepository.FindByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        if (user.Email != request.Email)
        {
            var emailExists = await _userRepository.EmailExistsAsync(request.Email);
            if (emailExists)
                throw new DuplicateResourceException("Já existe um usuário cadastrado com esse e-mail");
        }

        user.UpdateInfo(request.Name, request.Email, request.Role, request.Active);

        _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}