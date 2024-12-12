using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
             _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.OrderBy(u => u.CreatedAt).ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string name)
        {
            var users = await _context.Users
                .Where(u => u.Name.Contains(name))  
                .ToListAsync(); ;

            return users;
        }

        public async Task<User?> GetUserAsync(int Id)
        {
            var user = await _context.Users.FindAsync(Id);

            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var newUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return await _context.Users.FirstOrDefaultAsync(u => u.Id == newUser.Entity.Id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
