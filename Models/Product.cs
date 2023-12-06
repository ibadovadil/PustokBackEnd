using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBackEnd.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Brands { get; set; }
        [MaxLength(256)]
        public string? Title { get; set; }
        public int RewardPoint { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
        [Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
        public IEnumerable<ProductImages> ProductImages { get; set; }

    }
}