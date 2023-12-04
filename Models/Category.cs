using System.ComponentModel.DataAnnotations;

namespace PustokBackEnd.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
