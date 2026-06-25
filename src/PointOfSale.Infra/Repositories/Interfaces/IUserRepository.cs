using PointOfSale.Domain.Entities;

namespace PointOfSale.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailOrUsernameExistsAsync(string email, string username); 
        Task<bool> EmailExistsAsync(string email); 
        Task<User?> GetByEmailAsync(string email); 
        Task<User?> FindByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllActiveAsync();
        Task AddAsync(User user);
        void UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task<User?> FindByEmailOrUsernameAsync(string login);

    }
}
