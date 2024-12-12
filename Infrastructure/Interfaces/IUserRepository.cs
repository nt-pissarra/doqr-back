
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
    }
}
