using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokBackEnd.Areas.Admin.ViewModels;
using PustokBackEnd.Contexts;
using PustokBackEnd.Models;
using PustokBackEnd.ViewModels.ProductVM;

namespace PustokBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        PustokDBContext _db { get; }
        public ProductController(PustokDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            return View(_db.Products.Select(p => new AdminProductListItemVM
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Brands = p.Brands,
                RewardPoint = p.RewardPoint,
                SellPrice = p.SellPrice,
                CostPrice = p.CostPrice,
                Discount = p.Discount,
                Quantity = p.Quantity,
                Category = p.Category,
                IsDeleted = p.IsDeleted,
            }));
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {

            if (vm.CostPrice > vm.SellPrice)
            {
                ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            if (!await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            Product product = new Product
            {
                Name = vm.Name,
                Quantity = vm.Quantity,
                Description = vm.Description,
                Discount = vm.Discount,
                ImageUrl = vm.ImageUrl,
                CostPrice = vm.CostPrice,
                SellPrice = vm.SellPrice,
                CategoryId = vm.CategoryId
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
