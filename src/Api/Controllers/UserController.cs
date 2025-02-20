using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Domain.Enums;
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

        // Cadastrar-se (método específico)
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            if(!Enum.TryParse<Domain.Enums.UserType>(request.UserType.ToString(), true, out var userType))
            {
                return BadRequest("Tipo de usuário inválido");
            }
            else
            {
                await _userService.SignupAsync(request.Username, request.Email, request.Password, request.Name, userType);
                return Ok(new { message = "Usuário cadastrado com sucesso." });
            }
        }

        // Fazer Login (método específico)
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
