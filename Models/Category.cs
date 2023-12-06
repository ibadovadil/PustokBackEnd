using System.ComponentModel.DataAnnotations;

namespace PustokBackEnd.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public IEnumerable<Product>? Products { get; set; }
    }
}
