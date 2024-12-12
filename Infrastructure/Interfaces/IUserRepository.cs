
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
