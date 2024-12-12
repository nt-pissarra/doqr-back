using Application.Dtos;
using Application.Services;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.Interfaces;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_ShouldReturnUserDto_WhenUserIsCreated()
        {
            // Arrange: Criar um CreateUserDto
            var createUserDto = new CreateUserDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                CPF = "12345678900",
                Phone = "123456789",
                Mobile = "987654321",
                BirthDate = new DateTime(1990, 1, 1),
                EmploymentType = EmploymentType.PJ,
                Status = Status.Ativo
            };

            var user = new User
            {
                Id = 1,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                CPF = createUserDto.CPF,
                Phone = createUserDto.Phone,
                Mobile = createUserDto.Mobile,
                BirthDate = createUserDto.BirthDate,
                EmploymentType = createUserDto.EmploymentType,
                Status = createUserDto.Status
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
                              .ReturnsAsync(user); // Simulando a criação do usuário

            var userService = new UserService(mockUserRepository.Object);

            // Act: Chamar o método CreateUserAsync
            var result = await userService.CreateUserAsync(createUserDto);

            // Assert: Verificar se o usuário retornado tem os dados corretos
            Assert.NotNull(result);
            Assert.Equal(createUserDto.Name, result.Name);
            Assert.Equal(createUserDto.Email, result.Email);
        }
    }

}
