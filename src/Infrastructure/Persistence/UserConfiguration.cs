using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence
{  
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserType)
                .HasConversion<string>()  // Garante que o ENUM será salvo como texto
                .HasColumnType("text");   // Define o tipo da coluna no PostgreSQL
        }
    }
}