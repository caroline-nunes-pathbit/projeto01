using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByUserNameAsync(string name);
    }
}