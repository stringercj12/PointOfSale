using PointOfSale.Domain.Entities;

namespace PointOfSale.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> EmailOrUsernameExistsAsync(string email, string username);
    Task<bool> EmailExistsAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> FindByIdAsync(int userId);
    Task<IEnumerable<User>> GetAllActiveAsync(bool active);
    Task AddAsync(User user);
    void UpdateAsync(User user);
    void RemoveAsync(User user);
    Task<User?> FindByEmailOrUsernameAsync(string login);
    Task<int> CountActiveAdminsAsync();
}