using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.API.Models.Domain
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        // Many-to-Many Relationship with Product
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    }
}
