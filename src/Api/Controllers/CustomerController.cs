using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Api.Controllers
{
    [Route("api/customer")]
    public class CustomerController : BaseController<Customer>
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }
    }
}
