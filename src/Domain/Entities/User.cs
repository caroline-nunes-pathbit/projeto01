namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;
    }
    public enum UserType
    {
        Administrador,
        Cliente
    }
}