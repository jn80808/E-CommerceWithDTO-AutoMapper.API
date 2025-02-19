using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.API.Models.Domain
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        // One-to-Many Relationship with OrderItem
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
