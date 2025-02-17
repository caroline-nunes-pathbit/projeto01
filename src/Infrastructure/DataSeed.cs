using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(AppDbContext context,
            IUserRepository userRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository)
        {
            await context.Database.MigrateAsync();

            if (!context.Users.Any())
            {
                var admin = new User
                {
                    UserEmail = "admin@email.com",
                    UserName = "admin",
                    Password = ComputeSha256Hash("admin@123"),
                    UserType = "Admin"
                };

                var cliente = new User
                {
                    UserEmail = "cliente@email.com",
                    UserName = "cliente",
                    Password = ComputeSha256Hash("cliente@123"),
                    UserType = "Cliente"
                };

                await userRepository.AddAsync(admin);
                await userRepository.AddAsync(cliente);
            }

            if (!context.Customers.Any())
            {
                var customer1 = new Customer
                {
                    CustomerName = "Customer1",
                    CustomerEmail = "customer1@gmail.com"
                };

                var customer2 = new Customer
                {
                    CustomerName = "Customer2",
                    CustomerEmail = "customer2@gmail.com"
                };

                await customerRepository.AddAsync(customer1);
                await customerRepository.AddAsync(customer2);
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
            }

            if (!context.Orders.Any())
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
                        ProductId = product.First().Id
                    };

                    await orderRepository.AddAsync(order);
                }
            }
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
