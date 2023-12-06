namespace PustokBackEnd.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public int ProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public bool IsActive { get; set; } = false;

    }
}