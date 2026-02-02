using System.ComponentModel.DataAnnotations;

namespace RajLaxmiElectrical.Web.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}