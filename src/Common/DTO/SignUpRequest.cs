namespace Common.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class SignUpRequest
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;
    }
}
