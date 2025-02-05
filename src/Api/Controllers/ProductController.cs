using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Api.Controllers
{
    [Routes("api/product")]

    public class ProductController : ControllerBase<Product>
    {
        public ProductController(IGenericRepository<Product> repository) : base (repository) {}
    }
}