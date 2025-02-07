//Código genérico para controlar o CRUD das entidades (Resultados Ok ou Não encontrado)
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BaseController<T> ControllerBase where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public BaseController(IGenericRepository repository)
        {
            _repository = repository;
        }

        //Resultado da lista das entidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entites = await _repository.GetAllAsync();
            return Ok(entities);
        }

        //Resultado da lista de dados recuperados por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
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
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new {id = entity.GetType().GetProperty("Id")?.GetValue(entity)}, entity);

        }

        //Resultado da atualização de um dado
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id [FromBody] T entity)
        {
            if(entity is not null || entity.GetType().GetProperty("Id").GetValue(entity).ToString == id.ToString())
            {
                await _repository.UpdateAsync(entity);
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
            var entity = await _repository.GetByIdAsync(id);
            if(entity is not null)
            {
                return _repository.DeleteAsync(entity);
                return NoContent();
            } else 
            {
                return NotFound($"Id {id} não encontrado");
            }
        }
    }
}