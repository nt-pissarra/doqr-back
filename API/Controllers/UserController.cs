using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todos os usuários ou filtra por nome")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers([FromQuery] string? name)
        {
            var users = string.IsNullOrEmpty(name)
                ? await _userService.GetAllUsersAsync()
                : await _userService.GetUsersByNameAsync(name);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna um usuário pelo ID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound($"Usuário {id} não encontrado");
            }

            return Ok(user);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
        {
            var users = await _userService.CreateUserAsync(userDto);

            return CreatedAtAction(nameof(GetUser), new { id = users.Id }, users);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza informações de um usuário")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var user = await _userService.UpdateUserAsync(id, userDto);

            if (user == null)
            {
                return NotFound($"Usuário {id} não encontrado");
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário pelo ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);

            if (!success)
            {
                return NotFound($"Usuário {id} não encontrado");
            }

            return NoContent();
        }
    }
}
