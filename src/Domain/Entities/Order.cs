namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = null!;
        public decimal Total { get; set; }
        public string Cep { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!; //Chave estrangeira
        public ICollection<ProductOrder> ProductOrder { get; set; } = null!; //Relacionamento muitos para muitos
    }
    public enum Status
    {
        Enviado
    }
}