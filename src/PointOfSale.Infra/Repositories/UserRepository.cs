using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;
using PointOfSale.Infra.DataAccess;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PointOfSaleDbContext _context;

        public UserRepository(PointOfSaleDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> EmailOrUsernameExistsAsync(string email, string username)
        {
            return await _context.Users.AnyAsync(user => user.Email == email || user.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> FindByIdAsync(int userId)
        {
            return await _context.Users.Where(user => user.Id == userId).AsNoTracking().FirstOrDefaultAsync();
        }
        public void UpdateAsync(User user)
        {
            _context.Users.Update(user);
        }
        public void DeleteAsync(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllActiveAsync()
        {
             return await _context.Users
                .AsNoTracking()
                .Where(u => u.Active).ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(user => user.Email == email).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> FindByEmailOrUsernameAsync(string login)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == login || u.Username == login);
        }
    }
}
