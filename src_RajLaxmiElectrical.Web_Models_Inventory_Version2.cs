using System.ComponentModel.DataAnnotations;

namespace RajLaxmiElectrical.Web.Models
{
    public class Inventory
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        // Optional: supplier, location, reorder threshold
        public int ReorderThreshold { get; set; } = 10;
    }
}