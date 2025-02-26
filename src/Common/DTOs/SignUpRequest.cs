using Domain.Enums;

namespace Common.DTOs
{
public class SignUpRequest

    {
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string Password { get; set; } = null!;        
        public string UserType { get; set; } = null!;
    }
}
