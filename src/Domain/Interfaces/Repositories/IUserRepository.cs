using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAsync(string name);
        Task<User> GetByEmailAsync(string userEmail);
        Task<User> SignupAsync(string name, string userName, string userEmail, string password, UserType userType);
    }
}
