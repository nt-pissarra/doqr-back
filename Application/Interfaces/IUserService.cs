using Application.Dtos;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<List<UserDto>> GetUsersByNameAsync(string name);
        public Task<UserDto?> GetUserAsync(int Id);
        public Task<UserDto> CreateUserAsync(CreateUserDto user);
        public Task<UserDto?> UpdateUserAsync(int Id, UpdateUserDto user);
        public Task<bool> DeleteUserAsync(int Id);
    }
}
