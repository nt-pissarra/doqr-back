using Application.Dtos;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsersAsync();

        public Task<UserDto> CreateUserAsync(CreateUserDto user);
    }
}
