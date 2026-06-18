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

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> EmailOrUsernameExistsAsync(string email, string username)
        {
            return await _context.Users.AnyAsync(user => user.Email == email || user.Username == username);
        }

        public async Task<User?> FindByIdAsync(int userId)
        {
            return await _context.Users.Where(user => user.Id == userId).AsNoTracking().FirstOrDefaultAsync();
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
    }
}
