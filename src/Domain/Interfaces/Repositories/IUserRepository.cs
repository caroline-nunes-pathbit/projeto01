using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAsync(string name);
        Task<User> SignupAsync(string email, string username, string password, string name, string userType);
        Task<User> GetByEmailAsync(string email);
    }
}