using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces.Services;
using Application.DTOs;

namespace Api.Controllers
{
    [Route("api/order")]

    public class OrderController : BaseController<Order>
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) : base(orderService)
        {
            _orderService = orderService;
        }

        //Lista os pedidos
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _orderService.GetAllAsync();
            return Ok(order);
        }

        //Achar pedido por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
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
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            if(createOrderRequest is null)
            {
                return BadRequest("Dados inválidos.");
            }

            var order = await _orderService.CreateOrderAsync(
                createOrderRequest.CustomerId,
                createOrderRequest.ProductId,
                createOrderRequest.Quantity,
                createOrderRequest.Cep,
                createOrderRequest.UserType
            );

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);

        }

        //Editar um pedido
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] Order order)
        {
            if(id != order.Id)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _orderService.UpdateAsync(order);
                return NoContent();
            }
        }

        //Apagar um pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if(order is null)
            {
                return NotFound();
            } else 
            {
                await _orderService.DeleteAsync(id);
                return NoContent();
            }
        }
    }
}
