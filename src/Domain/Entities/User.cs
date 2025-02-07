namespace Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;
    }
}