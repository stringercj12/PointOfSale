using PointOfSale.Domain.Entities;

namespace PointOfSale.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailOrUsernameExistsAsync(string email, string username);
        Task<User?> GetByEmailAsync(string email);
        Task CreateAsync(User user);
    }
}
