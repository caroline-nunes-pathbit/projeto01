using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByUserNameAsync(string name);
        Task SignupAsync(string username, string email, string password, string name, UserType userType);
        Task<string> LoginAsync(string email, string password);
    }

}
