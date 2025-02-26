using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces.Services;
using Common.DTOs;
using Application.DTOs;

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

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var user = await _userService.GetByUserNameAsync(username);
                return Ok(user);
            }
            catch
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest request)
        {
            if (!Enum.TryParse<Domain.Enums.UserType>(request.UserType, true, out var userType))
            {
                return BadRequest(new { message = "Tipo de usuário inválido. Valores aceitos: Cliente, Administrador" });
            }

            try{
                await _userService.SignupAsync(request.Name, request.UserName, request.UserEmail, request.Password, userType);
                return Ok(new { message = "Usuário cadastrado com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.LoginAsync(loginRequest.UserEmail, loginRequest.Password);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }
        }
    }
}
