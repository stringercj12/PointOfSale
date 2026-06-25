using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Services.Common.Interfaces;
using PointOfSale.Domain.Entities;
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

        public async Task ExecuteAsync(int userId, string password)
        {
            var user = await _userRepository.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException("User not found");

            var newHash = _cryptoService.HashPassword(password);

            user.ChangePassword(newHash);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
