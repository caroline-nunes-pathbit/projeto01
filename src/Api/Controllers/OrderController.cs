namespace src.Api.Controllers
{
    [Routes("api/order")]

    public class OrderController : ControllerBase<Order>
    {
        public OrderController(IGenericRepository<Order> repository) : base (repository) {}
    }
}