using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAsync(string name);
        Task<User> SignupAsync(string email, string username, string password, UserType userType, string fullName);
        Task<User> GetByEmailAsync(string email);
    }
}
