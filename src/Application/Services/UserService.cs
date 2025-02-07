using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base (userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetByUserNameAsync(string name)
        {
            return await _userRepository.GetByUserNameAsync(name);
        }
    }
}