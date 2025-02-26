namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int QuantityAvaliable { get; set; }
        public Product() {}
    }
}