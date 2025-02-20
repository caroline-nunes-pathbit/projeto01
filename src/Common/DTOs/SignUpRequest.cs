using Domain.Enums;

namespace Common.DTOs
{
    public class SignUpRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserType { get; set; } = null!;
    }
}