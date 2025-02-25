using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByUserNameAsync(string name);
        Task SignupAsync(string name, string userName, string userEmail, string password, UserType userType);
        Task<string> LoginAsync(string userEmail, string password);
    }

}
