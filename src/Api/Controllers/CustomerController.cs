namespace src.Api.Controllers
{
    [Routes("api/customer")]

    public class CustomerController : ControllerBase<Customer>
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //Lista os clientes
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customer = await _customerService.GetAllAsync();
            return Ok(customer);
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
                await _customerService.AddAsync(customer);
                return CreatedAction(nameof(GetCustomerById), new {id = customer.CustomerId}, customer);
            }
        }

        //Editar um cliente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerController customer)
        {
            if(id != customer.CustomerId)
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
                await _customerService.DeleteAsync(customer);
                return NoContent();
            }
        }
    }
}