using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> GetByIsAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}