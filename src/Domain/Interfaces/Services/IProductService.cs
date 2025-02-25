//Interface que define a assinatura do método específico da entidade ProductService
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IProductService : IGenericService<Product>
    {
        Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max);
    }
}