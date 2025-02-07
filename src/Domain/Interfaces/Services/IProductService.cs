using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IProductService : IGenericService<Product>
    {
        Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max);
    }
}