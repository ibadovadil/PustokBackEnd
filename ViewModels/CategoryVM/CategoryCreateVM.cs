using System.ComponentModel.DataAnnotations;

namespace PustokBackEnd.ViewModels.CategoryVM
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(60)]
        public string Name { get; set; }
    }
}
