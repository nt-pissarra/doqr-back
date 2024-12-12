
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();

        public Task<IEnumerable<User>> GetUsersByNameAsync(string name);

        public Task<User?> GetUserAsync(int Id);

        public Task<User> CreateUserAsync(User user);

        public Task<User> UpdateUserAsync(User user);
        
        public Task<bool> DeleteUserAsync(User user);
    }
}
