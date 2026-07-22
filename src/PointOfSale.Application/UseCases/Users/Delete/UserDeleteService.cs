using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Enums;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Users.Delete;

public class UserDeleteService : IUserDeleteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UserDeleteService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int userId)
    {
        var user = await _userRepository.FindByIdAsync(userId);

        if (user is null)
            throw new NotFoundException("User not found");

        if (user.Role == RoleEnum.ADMIN)
        {
            var adminCount = await _userRepository.CountActiveAdminsAsync();
            if (adminCount <= 1)
                throw new BusinessException("Não é possível o administrador do sistema.");
        }

        user.ToggleActiveStatus();

        _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}