using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce.API.Models.Domain
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]  // Reference to the navigation property (Order)
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; } //navigation property

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
    }
}
