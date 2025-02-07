using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetByIsAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}