namespace Api.Controllers{

    [Routes("api/user")]

    public class UserController : ControllerBase<User>
    {
        private readonly IUserService _userService;
        public UserController(IUserService _userService) 
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
                return NotFound("Cliente não foi encontrado.");
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
                await _userService.AddAsync(user);
                return CreatedAction(nameof(GetUserById), new {id = user.UserId}, user);
            }
        }

        //Editar um usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserController user)
        {
            if(id != user.UserId)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
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
                await _userService.DeleteAsync(user);
                return NoContent();
            }
        }
    }
}