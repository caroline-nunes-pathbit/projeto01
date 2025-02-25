namespace Application.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}