using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Services.Common.Interfaces;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Application.Services.Users.Update
{
    public class UserChangePasswordService : IUserChangePasswordService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IUnitOfWork _unitOfWork;

        public UserChangePasswordService(IUserRepository userRepository, IUnitOfWork unitOfWork, ICryptoService cryptoService)
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
}
