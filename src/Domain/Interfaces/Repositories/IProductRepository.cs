//Interface que define a assinatura do método específico da entidade ProductRepository
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max);
    }
}