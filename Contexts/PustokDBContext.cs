using Microsoft.EntityFrameworkCore;
using PustokBackEnd.Models;

namespace PustokBackEnd.Contexts;

public class PustokDBContext : DbContext
{
    public PustokDBContext(DbContextOptions opt) : base(opt) { }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

}
