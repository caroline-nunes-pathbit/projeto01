using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max);
    }
}