using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        //Construtor para receber o AppDbContext e passar pra base GenericRepository
        public CustomerRepository(AppDbContext context) : base(context) {}
        //Método implementado para encontrar um cliente por nome
        public async Task<Customer> GetByCustomerNameAsync(string name){
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == name); 
            if(customer == null){
                throw new KeyNotFoundException($"O usuário {name} não foi encontrado.");
            } else
            {
                return customer;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Customers.AnyAsync(c => c.Id == id);
        }
    }
}
