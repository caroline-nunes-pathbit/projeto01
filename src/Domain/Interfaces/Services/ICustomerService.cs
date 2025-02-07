using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<Customer> GetByCustomerNameAsync(string name);
    }
}