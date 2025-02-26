//Interface que define a assinatura do método específico da entidade OrderRepository
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    }
}