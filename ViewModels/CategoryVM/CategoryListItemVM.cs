using PustokBackEnd.Models;

namespace PustokBackEnd.ViewModels.CategoryVM
{
    public class CategoryListItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Brands { get; set; }
        public string Title { get; set; }
        public int RewardPoint { get; set; }
        public decimal SellPrice { get; set; }
        public decimal CostPrice { get; set; }
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
