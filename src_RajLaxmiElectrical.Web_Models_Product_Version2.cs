using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RajLaxmiElectrical.Web.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? SKU { get; set; }

        public string? ImageUrl { get; set; }

        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public Inventory? Inventory { get; set; }
    }
}