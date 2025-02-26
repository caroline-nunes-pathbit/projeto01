using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services;
using Domain.Interfaces.Services;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;

public class UserService : GenericService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _jwtKey;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger, IConfiguration configuration) : base(userRepository)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
        _jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key não configurado.");
    }

    public async Task<string> LoginAsync(string userEmail, string password)
    {
        _logger.LogInformation("Tentando fazer login com o usuário: {UserEmail}", userEmail);

        var user = await _userRepository.GetByEmailAsync(userEmail);
        if(user is null || user.Password != password)
        {
            _logger.LogWarning("Usuário não encontrado: {UserEmail}", userEmail);
            throw new UnauthorizedAccessException("Usuário ou senha invalidos.");
        }

        return GenerateJwtToken(user);
    }

    public async Task SignupAsync(string name, string userName, string userEmail, string password, UserType userType)
    {
        var existingUser = await _userRepository.GetByEmailAsync(userEmail);
        if(existingUser is not null)
        {
            throw new InvalidOperationException("Email já cadastrado");
        }

        var user = new User
        {
            Name = name,
            UserName = userName,
            UserEmail = userEmail,
            Password = password,
            UserType = userType,
        };

        await _userRepository.AddAsync(user);
    }
    
    public async Task<User> GetByUserNameAsync(string name)
    {
        return await _userRepository.GetByUserNameAsync(name);
    }
    
    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
                new Claim("UserType", user.UserType.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature   )
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
