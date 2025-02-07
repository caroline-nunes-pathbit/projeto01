namespace src.Api.Controllers
{
    [Routes("api/product")]

    public class ProductController : ControllerBase<Product>
    {
        public ProductController(IGenericRepository<Product> repository) : base (repository) {}
    }
}