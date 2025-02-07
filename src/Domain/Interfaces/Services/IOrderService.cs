using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid id);
    }
}