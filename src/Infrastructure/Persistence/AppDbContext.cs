using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        //Definir tabelas
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        //Configurações de mapeamento
        protected override void ONConfiguring(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}