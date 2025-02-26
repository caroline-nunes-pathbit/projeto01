using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using System.Security.Cryptography;
using System.Text;
using Domain.Enums;

namespace Infrastructure
{
    public class DataSeed
    {
        private readonly ILogger<DataSeed> _logger;

        public DataSeed(ILogger<DataSeed> logger)
        {
            _logger = logger;
        }

        public async Task SeedDataAsync(AppDbContext context,
            IUserRepository userRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository)
        {
            _logger.LogInformation("Starting data seeding process.");
            await context.Database.MigrateAsync();

            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Name = "Administrador do Sistema",
                    UserName = "admin",
                    UserEmail = "admin@email.com",
                    Password = ComputeSha256Hash("admin@123"),
                    UserType = UserType.Administrador,
                };

                var cliente = new User
                {
                    Name = "Cliente Exemplo",
                    UserName = "cliente",
                    UserEmail = "cliente@email.com",
                    Password = ComputeSha256Hash("cliente@123"),
                    UserType = UserType.Cliente,
                };

                await userRepository.AddAsync(admin);
                await userRepository.AddAsync(cliente);
                _logger.LogInformation("Users seeded successfully.");
            }
            else
            {
                _logger.LogWarning("Users already exist, skipping user seeding.");
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { ProductName = "Produto1", Price = 20, QuantityAvaliable = 90 },
                    new Product { ProductName = "Produto2", Price = 320, QuantityAvaliable = 90 },
                    new Product { ProductName = "Produto3", Price = 50, QuantityAvaliable = 90 }
                };

                foreach (var product in products)
                {
                    await productRepository.AddAsync(product);
                }
                _logger.LogInformation("Products seeded successfully.");
            }
            else
            {
                _logger.LogWarning("Products already exist, skipping product seeding.");
            }

            if (!context.Orders.Any() && context.Users.Any(u => u.UserType == UserType.Cliente))
            {
                var customer = await customerRepository.GetAllAsync();
                var product = await productRepository.GetAllAsync();

                if (customer.Any() && product.Any())
                {
                    var order = new Order
                    {
                        Status = "Enviado",
                        Total = 100,
                        Cep = "12345-678",
                        Address = "Rua Exemplo, 123",
                        CustomerId = customer.First().Id,
                        ProductId = product.First().Id,
                        OrderDate = DateTime.UtcNow // Convertendo para UTC
                    };

                    await orderRepository.AddAsync(order);
                    _logger.LogInformation("Orders seeded successfully.");
                }
                else
                {
                    _logger.LogWarning("No customers or products found to seed orders.");
                }
            }

            var clientes = context.Users.Where(u => u.UserType == UserType.Cliente).ToList();
            foreach (var cliente in clientes)
            {
                if (!await customerRepository.ExistsAsync(cliente.Id))
                {
                    var customer = new Customer
                    {
                        CustomerName = cliente.Name,
                        CustomerEmail = cliente.UserEmail,
                    };
                    await customerRepository.AddAsync(customer);
                    _logger.LogInformation($"Customer {cliente.Name} added.");
                }
            }

            _logger.LogInformation("Data seeding process completed.");
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
