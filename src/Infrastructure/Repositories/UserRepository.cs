using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

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
        public async Task<User> SignupAsync(string userEmail, string username, string password, string userType, string fullName)
        {
            var user = new User
            {
                UserEmail = userEmail,
                UserName = username,
                Password = password,
                UserType = userType,
                FullName = fullName
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        //Método implementado para encontrar um cliente pelo email
        public async Task<User> GetByEmailAsync(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == userEmail);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            return user;
        }
    }
}
