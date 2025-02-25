using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        //Método implementado para encontrar um usuário por nome
        public async Task<User> GetByUserNameAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            return user;
        }

        //Implementação do método de Cadastrar
        public async Task<User> SignupAsync(string name, string userName, string userEmail, string password, UserType userType)
        {
            var user = new User
            {
                Name = name,
                UserName = userName,
                UserEmail = userEmail,
                Password = password,
                UserType = userType,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //Método implementado para encontrar um cliente pelo email
        public async Task<User> GetByEmailAsync(string userEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == userEmail);
        }
    }
}
