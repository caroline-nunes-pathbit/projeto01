//Controller genérico para as entidades
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BaseController<T> ControllerBase where T : class
    {
        private readonly IGenericService<T> _service;

        public BaseController(IGenericService service)
        {
            _service = service;
        }

        //Resultado da lista das entidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entity = await _service.GetAllAsync();
            return Ok(entity);
        }

        //Resultado da lista de dados recuperados por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if(entity is not null)
            {
                return Ok(entity);
            } else 
            {
                return NotFound($"Id {id} não encontrado");
            }
        }

        //Resultado da inserção de um dado
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T entity)
        {
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new {id = entity.GetType().GetProperty("Id")?.GetValue(entity)}, entity);

        }

        //Resultado da atualização de um dado
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] T entity)
        {
            if(entity is not null || entity.GetType().GetProperty("Id").GetValue(entity).ToString == id.ToString())
            {
                await _service.UpdateAsync(entity);
                return NoContent();
            } else 
            {
                return BadRequest($"Id {id} inválido");
            }
        }
        //Resultado ao deletar um dado
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if(entity is not null)
            {
                return _service.DeleteAsync(entity);
                return NoContent();
            } else 
            {
                return NotFound($"Id {id} não encontrado");
            }
        }
    }
}