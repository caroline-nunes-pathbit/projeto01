using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) : base (productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max)
        {
            return await _productRepository.GetByPriceAsync(min, max);
        }
    }
}