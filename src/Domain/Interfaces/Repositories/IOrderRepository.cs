//Interface que define a assinatura do método específico da entidade OrderRepository
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    }
}