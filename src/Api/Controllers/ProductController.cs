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

        // Lista os produtos
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _productService.GetAllAsync();
            return Ok(product);
        }

        // Achar produto por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product is null)
            {
                return NotFound("Produto não foi encontrado.");
            }
            return Ok(product);
        }

        // Criar um produto
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDto)
        {
            if(productDto.Quantity <= 0)
            {
                return BadRequest("A quantidade do produto deve ser maior que zero.");
            }
            
            var product = new Product
            {
                ProductName = productDto.Name,
                Price = productDto.Price,
                QuantityAvaliable = productDto.Quantity
            };

            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetProductById), new {id = product.Id}, product);
        }

        // Editar um produto
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            if(id != product.Id)
            {
                return BadRequest("Dados inválidos.");
            }
            
            await _productService.UpdateAsync(product);
            return NoContent();
        }

        // Apagar um produto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product is null)
            {
                return NotFound();
            }
            
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
