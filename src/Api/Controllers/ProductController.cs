using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Domain.Interfaces.Services;
using Application.DTOs;

namespace Api.Controllers
{
    [Route("api/product")]
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        // Sobrescrever o método Create para lidar com produtos
        [Authorize(Roles = "Administrador")]
        public override async Task<IActionResult> Create([FromBody] Product product)
        {
            if(product == null || product.QuantityAvaliable <= 0)
            {
                return BadRequest("Dados inválidos. A quantidade deve ser maior que zero.");
            }

            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new {id = product.Id}, product);
        }
    }
}
