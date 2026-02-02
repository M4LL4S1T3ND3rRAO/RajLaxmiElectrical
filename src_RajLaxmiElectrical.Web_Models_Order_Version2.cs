using System.ComponentModel.DataAnnotations;

namespace RajLaxmiElectrical.Web.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Status { get; set; } = "Pending";

        public ICollection<OrderItem>? Items { get; set; }
    }
}