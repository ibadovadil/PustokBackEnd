using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBackEnd.Contexts;
using PustokBackEnd.ViewModels.CategoryVM;

namespace PustokBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDBContext _db { get; }

        public CategoryController(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Categories.Select(c => new CategoryListItemVM { Id = c.Id, Name = c.Name }).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await _db.Categories.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "This Name already Exist");
                return View(vm);
            }
            await _db.Categories.AddAsync(new Models.Category { Name = vm.Name });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
