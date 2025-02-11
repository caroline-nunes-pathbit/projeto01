using System.Text.Json;
using System.Transactions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly HttpClient _httpClient;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, HttpClient httpClient) : base (orderRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _httpClient = httpClient;
        }
        //Criar um pedido
        public async Task<Order> CreateOrderAsync(Guid customerId, Guid productId, int quantity, string cep, UserType userType)
        {
            if(userType != UserType.Cliente)
            {
                throw new UnauthorizedAccessException("Apenas clientes podem realizar pedidos.");
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if(product is null)
            {
                throw new Exception("Produto não foi encontrado.");
            }
            
            if(product.QuantityAvaliable < quantity)
            {
                throw new Exception("Quantidade insuficiente no estoque.");
            }

            var address = await GetAddressFromApiAsync(cep);
            if(string.IsNullOrEmpty(address))
            {
                throw new Exception("Endereço não encontrado.");
            } 

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var order = new Order
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    OrderDate = DateTime.UtcNow,
                    Status = "Enviado",
                    Total = product.Price * quantity,
                    Cep = cep,
                    Address = address
                };
                await _orderRepository.AddAsync(order);
                product.QuantityAvaliable -= quantity;
                await _productRepository.UpdateAsync(product);
                transaction.Complete();
                return order;
            }
        }
        //Pegar endereço da API externa
        private async Task<string> GetAddressFromApiAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://api.ceprapido.com/{cep}");
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Endereço não encontrado.");
            }
            var json = await response.Content.ReadAsStringAsync();
            var addressData = JsonSerializer.Deserialize<AddressResponse>(json);
            if(addressData is null || string.IsNullOrEmpty(addressData.Address))
            {
                throw new Exception("Endereço não encontrado.");
            }
            return addressData.Address;
        }
        //Implementação de achar cliente por id
        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid id)
        {
            return await _orderRepository.GetByCustomerIdAsync(id);
        }

        public class AddressResponse
        {
            public string Address { get; set; } = string.Empty;
        }
    }
}
