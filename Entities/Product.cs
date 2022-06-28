using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal MarkedPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
        public int Stock { get; set; }
    }
    public class ProductAddModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MarkedPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; } = 0;
    }
}
