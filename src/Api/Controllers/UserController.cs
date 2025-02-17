using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Domain.Interfaces.Services;
using Application.DTOs;
using Common.DTOs;

namespace Api.Controllers
{

    [Route("api/user")]

    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) : base(userService)

        {
            _userService = userService;
        }

        //Lista os usuários
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllAsync();
            return Ok(user);
        }

        //Achar usuário por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if(user is null)
            {
                return NotFound(new { message = "Usuário não foi encontrado." });

            } else
            {
                return Ok(user);
            }
        }

        //Criar um usuário
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if(user is null)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _userService.CreateAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);

            }
        }

        //Editar um usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            if(id != user.Id)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                var existingUser = await _userService.GetByIdAsync(id);
                if (existingUser is null)
                {
                return NotFound(new { message = "Usuário não encontrado." });

                }
                await _userService.UpdateAsync(user);

                return NoContent();
            }
        }

        //Apagar um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if(user is null)
            {
                return NotFound();
            } else 
            {
                await _userService.DeleteAsync(id);
                return NoContent();
            }
        }

        //Cadastrar-se
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp ([FromBody] SignUpRequest request)
        {
            try
            {
                await _userService.RegisterUserAsync(request);
            return Ok(new { message = "Usuário cadastrado com sucesso." });

            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        //Fazer Login 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);
                return Ok(new { token });

            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });

            }
        }
    }
}
