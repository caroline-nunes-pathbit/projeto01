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

        public async Task<Customer> GetByCustomerNameAsync(string name){
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == name); 
            if(customer == null){
                throw new KeyNotFoundException($"O usuário {name} não foi encontrado.");
            } else
            {
                return customer;
            }
            
        }
    }
}