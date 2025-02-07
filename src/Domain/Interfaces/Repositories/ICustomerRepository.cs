using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetByCustomerNameAsync(string name);
    }
}