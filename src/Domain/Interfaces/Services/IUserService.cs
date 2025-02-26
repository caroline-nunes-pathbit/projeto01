using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<string> LoginAsync(string userEmail, string password);
        Task SignupAsync(string name, string userName, string userEmail, string password, UserType userType);
        Task<User> GetByUserNameAsync(string name);

    }

}
