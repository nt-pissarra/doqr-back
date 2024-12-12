

using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = ModelToDto(user);
                usersDto.Add(userDto);
            }

            return usersDto;
        }

        public async Task<List<UserDto>> GetUsersByNameAsync(string name)
        {
        
            var users = await _userRepository.GetUsersByNameAsync(name);

            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = ModelToDto(user);
                usersDto.Add(userDto);
            }

            return usersDto;
        }

        public async Task<UserDto?> GetUserAsync(int Id)
        {
            var user = await _userRepository.GetUserAsync(Id);

            if (user == null) return null;

            return ModelToDto(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            var newUser = DtoToCreateUserModel(user);

            var createdUser = await _userRepository.CreateUserAsync(newUser);

            return ModelToDto(createdUser);
        }

        public async Task<UserDto?> UpdateUserAsync(int Id, UpdateUserDto userDto)
        {
            var user = await _userRepository.GetUserAsync(Id);

            if (user == null)
            {
                return null; 
            }

            user = UpdateUserDtoToModel(user, userDto);

            await _userRepository.UpdateUserAsync(user);

            return ModelToDto(user);
        }

        public async Task<bool> DeleteUserAsync(int Id)
        {
            var user = await _userRepository.GetUserAsync(Id);

            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteUserAsync(user);

            return true;
        }


        private static User UpdateUserDtoToModel(User user, UpdateUserDto userDto)
        {
            user.Name = string.IsNullOrEmpty(userDto.Name) ? user.Name : userDto.Name;
            user.Email = string.IsNullOrEmpty(userDto.Email) ? user.Email : userDto.Email;
            user.CPF = string.IsNullOrEmpty(userDto.CPF) ? user.CPF : userDto.CPF;
            user.Phone = string.IsNullOrEmpty(userDto.Phone) ? user.Phone : userDto.Phone;
            user.BirthDate = !userDto.BirthDate.HasValue ? user.BirthDate : userDto.BirthDate!.Value;
            user.EmploymentType = !userDto.EmploymentType.HasValue ? user.EmploymentType : userDto.EmploymentType!.Value;
            user.Status = !userDto.Status.HasValue ? user.Status : userDto.Status!.Value;
            user.UpdatedAt = DateTime.UtcNow;

            return user;
        }


        private static User DtoToCreateUserModel(CreateUserDto user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                CPF = user.CPF,
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                EmploymentType = user.EmploymentType,
                Status = user.Status
            };
        }

        private static UserDto ModelToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CPF = user.CPF,
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                EmploymentType = user.EmploymentType,
                Status = user.Status
            };
        }
    }

}
