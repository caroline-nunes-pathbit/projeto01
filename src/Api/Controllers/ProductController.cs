namespace src.Api.Controllers
{
    [Routes("api/product")]

    public class ProductController : ControllerBase<Product>
    {
        private readonly IProductService _productService;
        public ProductController(IProductService _productService) 
        {
            _productService = productService;
        }

        //Lista os produtos
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _productService.GetAllAsync();
            return Ok(product);
        }

        //Achar produto por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product is null)
            {
                return NotFound("Produto não foi encontrado.");
            } else
            {
                return Ok(product);
            }
        }

        //Criar um produto
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if(product is null)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _productService.AddAsync(product);
                return CreatedAction(nameof(GetProductById), new {id = product.ProductId}, product);
            }
        }

        //Editar um cliente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductController product)
        {
            if(id != product.ProductId)
            {
                return BadRequest("Dados inválidos.");
            } else
            {
                await _productService.UpdateAsync(product);
                return NoContent();
            }
        }

        //Apagar um cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product is null)
            {
                return NotFound();
            } else 
            {
                await _productService.DeleteAsync(product);
                return NoContent();
            }
        }
    }
}