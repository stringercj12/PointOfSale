using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Users.Update;

public class UserChangePasswordService : IUserChangePasswordService
{
    private readonly ICryptoService _cryptoService;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserRepository _userRepository;

    public UserChangePasswordService(IUserRepository userRepository, IUnitOfWork unitOfWork,
        ICryptoService cryptoService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _cryptoService = cryptoService;
    }

    public async Task ExecuteAsync(ResetPasswordRequest request)
    {
        var user = await _userRepository.FindByIdAsync(request.UserId);

        if (user is null)
            throw new NotFoundException("User not found");

        var newHash = _cryptoService.HashPassword(request.NewPassword);

        user.ChangePassword(newHash);

        _userRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }
}