using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Api.Controllers
{
    [Route("api/customer")]
    public class CustomerController : BaseController<Customer>
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        //Achar cliente por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if(customer is null)
            {
                return NotFound("Cliente não foi encontrado.");
            } else
            {
                return Ok(customer);
            }
        }

        //Criar um cliente
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if(customer is null)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _customerService.CreateAsync(customer);
                return CreatedAtAction(nameof(GetCustomerById), new {id = customer.Id}, customer);
            }
        }

        //Editar um cliente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            if(id != customer.Id)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _customerService.UpdateAsync(customer);
                return NoContent();
            }
        }

        //Apagar um cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if(customer is null)
            {
                return NotFound();
            } else 
            {
                await _customerService.DeleteAsync(id);
                return NoContent();
            }
        }
    }
}
