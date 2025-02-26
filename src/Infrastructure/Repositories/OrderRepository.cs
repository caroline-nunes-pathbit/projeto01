using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        //Construtor para receber o AppDbContext e passar pra base GenericRepository
        public OrderRepository(AppDbContext context) : base(context) {}
        //Método implementado para encontrar um pedido pelo Id do cliente
        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customer)
        {
            return await _context.Orders.Where(o => o.CustomerId == customer).ToListAsync();
        }
    }
}