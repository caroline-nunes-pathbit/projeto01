//Interface que define a assinatura do método específico da entidade UserService
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByUserNameAsync(string name);
    }
}