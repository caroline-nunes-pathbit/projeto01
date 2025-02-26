namespace Application.DTOs
{
    public class LoginRequest
    {
        public string UserEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
