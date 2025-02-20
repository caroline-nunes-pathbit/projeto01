using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Cep { get; set; } = null!;
        public UserType UserType { get; set; }
    }
}
