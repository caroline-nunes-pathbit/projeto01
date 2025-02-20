using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IConfiguration configuration) : base (userRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _configuration = configuration;
        }
        //Implementação do método de achar usuário por nome
        public async Task<User> GetByUserNameAsync(string name)
        {
            return await _userRepository.GetByUserNameAsync(name);
        }
        //Cadastrar
        public async Task SignupAsync(string username, string email, string password, string name, UserType userType)
        {
            Console.WriteLine($"Tentando cadastrar usuário com email: {email}");
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new Exception("Email já cadastrado.");
            }
        
            var hashPassword = ComputeSha256Hash(password);
            
            var user = new User
            {
                UserEmail = email,
                UserName = username,
                Password = hashPassword,
                UserType = userType
            };

            await _userRepository.AddAsync(user);

            if (userType == UserType.Cliente)
            {
                var customer = new Customer
                {
                    CustomerEmail = email,
                    CustomerName = name
                };
                await _customerRepository.AddAsync(customer);
            }
        }


        //Fazer Login
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if(user is null)
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");
            }
            string hashPassword = ComputeSha256Hash(password);
            if(user.Password != hashPassword)
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");
            }
            var token = GenerateJwtToken(user);
            return token;
        }
        // Hash Password que retorna hexadecimal
        private string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-","").ToLower();
            }
        }

        //Gerar token JWT
        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["JwtSettings:Key"];
            if(string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key não configurado.");
            }
            var key = Encoding.UTF8.GetBytes(jwtKey);
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            if(string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("Issuer ouAudience não configurados.");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
                new Claim("name", user.UserName)
            };
            
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
