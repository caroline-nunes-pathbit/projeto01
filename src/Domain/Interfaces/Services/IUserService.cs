using Domain.Entities;
using Common.DTOs;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByUserNameAsync(string name);
        Task RegisterUserAsync(SignUpRequest request);
        Task<string> LoginAsync(string email, string password);
    }

}
