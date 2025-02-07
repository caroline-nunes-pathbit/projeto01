using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> GetByIsAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}