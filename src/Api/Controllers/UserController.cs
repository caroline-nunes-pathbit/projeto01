namespace Api.Controllers{

    [Routes("api/user")]

    public class UserController : ControllerBase<User>
    {
        public UserController(IGenericRepository<User> repository) : base (repository) {}
    }
}