namespace src.Api.Controllers
{
    [Routes("api/customer")]

    public class CustomerController : ControllerBase<Customer>
    {
        public CustomerController(IGenericRepository<Customer> repository) : base (repository) {}
    }
}