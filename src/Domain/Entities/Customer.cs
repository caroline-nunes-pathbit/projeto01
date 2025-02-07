namespace Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
    }
}