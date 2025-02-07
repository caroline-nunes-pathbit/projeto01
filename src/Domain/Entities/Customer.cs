namespace Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
    }
}