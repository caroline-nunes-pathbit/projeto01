//Interface que define a assinatura do método específico da entidade CustomerService
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<Customer> GetByCustomerNameAsync(string name);
    }
}