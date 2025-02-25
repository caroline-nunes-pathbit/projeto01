//Interface que define a assinatura do método específico da entidade CustomerRepository
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetByCustomerNameAsync(string name);
        Task<bool> ExistsAsync(Guid id);
    }
}
