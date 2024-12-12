

using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
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

        public async Task<UserDto?> GetUserAsync(int Id)
        {
            var user = await _userRepository.GetUserAsync(Id);

            if (user == null) return null;

            var userDto = ModelToDto(user);

            return userDto;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            var newUser = DtoToCreateUserModel(user);

            var createdUser = await _userRepository.CreateUserAsync(newUser);

            var userDto = ModelToDto(createdUser);

            return userDto;
        }


        private User DtoToCreateUserModel(CreateUserDto user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                CPF = user.CPF,
                Phone = user.Phone,
                Mobile = user.Mobile,
                BirthDate = user.BirthDate,
                EmploymentType = user.EmploymentType,
                Status = user.Status
            };
        }



        private UserDto ModelToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CPF = user.CPF,
                Phone = user.Phone,
                Mobile = user.Mobile,
                BirthDate = user.BirthDate,
                EmploymentType = user.EmploymentType,
                Status = user.Status
            };
        }

        private User DtoToModel(UserDto userDto)
        {
            return new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                CPF = userDto.CPF,
                Phone = userDto.Phone,
                Mobile = userDto.Mobile,
                BirthDate = userDto.BirthDate,
                EmploymentType = userDto.EmploymentType,
                Status = userDto.Status
            };
        }
    }

}
