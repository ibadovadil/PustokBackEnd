using PustokBackEnd.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PustokBackEnd.Areas.Admin.ViewModels
{
    public class AdminProductListItemVM
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Brands { get; set; }
        public int RewardPoint { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
        [Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
