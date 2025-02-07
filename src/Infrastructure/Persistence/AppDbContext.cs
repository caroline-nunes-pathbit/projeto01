using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence;

    public class AppDbContext : DbContext
    {
        //Definir tabelas
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) {}
        
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        //Configurações de mapeamento
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            //Relacionamentos muitos para muitos
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId});

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.ProductOrder)
                .HasForeignKey(po => po.OrderId);
            
            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductOrder)
                .HasForeignKey(po => po.ProductId);
        }
    }