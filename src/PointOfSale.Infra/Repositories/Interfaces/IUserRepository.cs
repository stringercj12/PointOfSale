using PointOfSale.Domain.Entities;

namespace PointOfSale.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailOrUsernameExistsAsync(string email, string username);
        Task<User?> GetByEmailAsync(string email); 
        Task<User?> FindByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllActiveAsync();
        Task CreateAsync(User user);
    }
}
