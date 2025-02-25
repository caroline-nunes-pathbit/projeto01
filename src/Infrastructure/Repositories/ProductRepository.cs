using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        //Construtor para receber o AppDbContext e passar pra base GenericRepository
        public ProductRepository(AppDbContext context) : base(context) {}
        //Método implementado para encontrar um produto pelo preço
        public async Task<IEnumerable<Product>> GetByPriceAsync(decimal min, decimal max)
        {
            return await _context.Products.Where(p => p.Price >= min && p.Price <= max).ToListAsync();
        }
    }
}