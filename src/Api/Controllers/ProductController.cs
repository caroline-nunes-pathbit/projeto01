namespace src.Api.Controllers
{
    [Routes("api/product")]

    public class ProductController : ControllerBase<Product>
    {
        public ProductController(IGenericRepository<Product> product) : base (product) 
        {
            _productRepository = productRepository;
        }

        //Lista os produtos
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _productRepository.GetAllAsync();
            return Ok(product);
        }

        //Achar produto por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
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
                await _productRepository.AddAsync(product);
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
                await _productRepository.UpdateAsync(product);
                return NoContent();
            }
        }

        //Apagar um cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product is null)
            {
                return NotFound();
            } else 
            {
                await _productRepository.DeleteAsync(product);
                return NoContent();
            }
        }
    }
}