using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Api.Controllers
{
    [Routes("api/customer")]

    public class CustomerController : ControllerBase<Customer>
    {
        public CustomerController(IGenericRepository<Customer> repository) : base (repository) {}
    }
}