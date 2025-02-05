namespace Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string Cep { get; set; }
        public string Adress { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}