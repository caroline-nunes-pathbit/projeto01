//Tabela muitos para muitos
namespace Domain.Entities
{
    public class ProductOrder
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        //Chaves estrangeiras
        public Product Product { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}