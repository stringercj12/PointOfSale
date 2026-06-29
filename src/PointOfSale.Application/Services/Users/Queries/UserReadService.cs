using PointOfSale.Application.DTOs.Response.Users;
using PointOfSale.Application.Exceptions;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Application.Services.Users.Get
{
    public class UserReadService : IUserReadService
    {
        private readonly IUserRepository _userRepository;

        public UserReadService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> FindByIdAsync(int userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException("Usuário não encontrado");

            return UserResponse.From(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllActiveAsync(bool active = true)
        {
            var users = await _userRepository.GetAllActiveAsync(active);

            return users.Select(user => UserResponse.From(user)).ToList();
        }
    }
}
