namespace src.Api.Controllers
{
    [Routes("api/order")]

    public class OrderController : ControllerBase<Order>
    {
        public OrderController(IGenericRepository<Order> order) : base (order) 
        {
            _orderRepository = orderRepository;
        }

        //Lista os pedidos
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _orderRepository.GetAllAsync();
            return Ok(order);
        }

        //Achar pedido por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if(order is null)
            {
                return NotFound("Pedido não foi encontrado.");
            } else
            {
                return Ok(order);
            }
        }

        //Criar um pedido
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if(order is null)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _orderRepository.AddAsync(order);
                return CreatedAction(nameof(GetOrderById), new {id = order.OrderId}, order);
            }
        }

        //Editar um pedido
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrederController order)
        {
            if(id != order.OrderId)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _orderRepository.UpdateAsync(order);
                return NoContent();
            }
        }

        //Apagar um pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if(order is null)
            {
                return NotFound();
            } else 
            {
                await _orderRepository.DeleteAsync(oredr);
                return NoContent();
            }
        }
    }
}