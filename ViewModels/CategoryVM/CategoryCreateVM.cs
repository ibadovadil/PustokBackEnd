using PustokBackEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokBackEnd.ViewModels.CategoryVM
{
    public class CategoryCreateVM
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
