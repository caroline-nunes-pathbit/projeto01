using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid id);
        Task<Order> CreateOrderAsync(Guid customerId, Guid productId, int quantity, string cep, UserType userType);
    }
}
