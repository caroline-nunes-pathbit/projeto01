using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) : base (customerRepository)
        {
            _customerRepository = customerRepository;
        }
        //Achar cliente por nome
        public async Task<Customer> GetByCustomerNameAsync(string name)
        {
            return await _customerRepository.GetByCustomerNameAsync(name);
        }
    }
}