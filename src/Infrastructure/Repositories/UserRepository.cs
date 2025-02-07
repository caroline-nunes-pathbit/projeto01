using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //Construtor para receber o AppDbContext e passar pra base GenericRepository
        public UserRepository(AppDbContext context) : base(context) {}

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
            if(user == null){
                throw new KeyNotFoundException($"O usuário com email {email} não foi encontrado.");
            } else
            {
                return user;
            }
            
        }
    }
}