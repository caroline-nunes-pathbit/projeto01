using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Api.Controllers
{
    [Routes("api/order")]

    public class OrderController : ControllerBase<Order>
    {
        public OrderController(IGenericRepository<Order> repository) : base (repository) {}
    }
}