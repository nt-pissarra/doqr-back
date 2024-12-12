
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();

        public Task<User> GetUserAsync(int Id);

        public Task<User> CreateUserAsync(User user);
    }
}
