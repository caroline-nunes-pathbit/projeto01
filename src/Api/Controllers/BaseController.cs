using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        protected readonly IGenericService<T> _service;

        public BaseController(IGenericService<T> service)
        {
            _service = service;
        }

        // Resultado da lista das entidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        // Resultado da lista de dados recuperados por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity is not null)
            {
                return Ok(entity);
            }
            return NotFound($"Id {id} não encontrado");
        }

        // Resultado da inserção de um dado
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] T entity)
        {
            if (entity is null)
            {
                return BadRequest("Entidade inválida");
            }

            await _service.CreateAsync(entity);

            var entityId = entity.GetType().GetProperty("Id")?.GetValue(entity);

            return CreatedAtAction(nameof(GetById), new { id = entityId }, entity);
        }

        // Resultado da atualização de um dado
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] T entity)
        {
            if (entity is null || entity.GetType().GetProperty("Id")?.GetValue(entity)?.ToString() != id.ToString())
            {
                return BadRequest($"Id {id} inválido");
            }

            await _service.UpdateAsync(entity);
            return NoContent();
        }

        // Resultado ao deletar um dado
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound($"Id {id} não encontrado");
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
