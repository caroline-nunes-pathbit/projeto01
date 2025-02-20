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

        // Sobrescrever o método Create para lidar com pedidos
        public override async Task<IActionResult> Create([FromBody] Order order)
        {
            if(order is null)
            {
                return BadRequest("Dados inválidos.");
            }

            // Lógica específica para criação de pedidos
            await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
    }
}
