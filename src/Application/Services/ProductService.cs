using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;
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
        //Criar um produto
        public async Task<Product> CreateProductAsync(ProductDTO productDto)
        {
            if(productDto.Quantity <= 0)
            {
                throw new ArgumentException("A quantidade do produto deve ser maior que zero.");
            } else 
            {
                var product = new Product()
                {
                    ProductName = productDto.Name,
                    Price = productDto.Price,
                    QuantityAvaliable = productDto.Quantity
                };
                await _productRepository.AddAsync(product);
                return product;
            }
        }
        //Implementação do método de achar produto pelo preço
        public async Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max)
        {
            return await _productRepository.GetByPriceAsync(min, max);
        }
    }
}