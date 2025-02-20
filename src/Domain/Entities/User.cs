using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        [Column(TypeName = "text")]
        public UserType UserType { get; set; }
        public string FullName { get; set; } = null!;
    }
}