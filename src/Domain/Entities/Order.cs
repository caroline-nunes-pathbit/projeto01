namespace Domain.Entities
{
    public class Order
    {
        // As propriedades Quantity e Price foram removidas, pois estão na classe Product.


    
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = null!;
        public decimal Total { get; set; }
        public string Cep { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

        public Customer Customer { get; set; } = null!; //Chave estrangeira
        public Product Product { get; set; } = null!; //Chave estrangeira
    }
    public enum Status
    {
        Enviado
    }
}
