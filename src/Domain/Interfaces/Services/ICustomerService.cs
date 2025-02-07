using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetByIsAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
    }
}