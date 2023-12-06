//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PustokBackEnd.Contexts;
//using PustokBackEnd.ViewModels.CategoryVM;

//namespace PustokBackEnd.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class CategoryController : Controller
//    {
//        PustokDBContext _db { get; }

//        public CategoryController(PustokDBContext db)
//        {
//            _db = db;
//        }

//        public async Task<IActionResult> Index()
//        {
//            return View(await _db.Categories.Select(c => new CategoryListItemVM { Id = c.Id, Name = c.Name }).ToListAsync());
//        }
//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create(CategoryCreateVM vm)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(vm);
//            }
//            if (await _db.Categories.AnyAsync(x => x.Name == vm.Name))
//            {
//                ModelState.AddModelError("Name", "This Name already Exist");
//                return View(vm);
//            }
//            await _db.Categories.AddAsync(new Models.Category { Name = vm.Name });
//            await _db.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));

//        }


//        public async Task<IActionResult> Delete(int? Id)
//        {
//            TempData["Respnose"] = false;
//            if (Id == null) return BadRequest();
//            var data = await _db.Categories.FindAsync(Id);
//            if (data == null) return NotFound();
//            _db.Categories.Remove(data);
//            await _db.SaveChangesAsync();
//            TempData["Respnose"] = true;
//            return RedirectToAction(nameof(Index));
//        }
//        public async Task<IActionResult> Update(int? Id)
//        {
//            if (Id == null || Id <= 0) return BadRequest();
//            var data = await _db.Categories.FindAsync(Id);
//            if (data == null) return NotFound();
//            return View(new CategoryUpdateVM
//            {
//                Name = data.Name,
//                ParentCategoryId = data.ParentCategoryId,
//                Products = data.Products,
//                //IsDeleted = data.IsDeleted switch
//                //{
//                //    true => 1,
//                //    false => 0,
//                //},
//            });
//        }

//        [HttpPost]
//        public async Task<IActionResult> Update(int? Id, CategoryUpdateVM vm)
//        {
//            if (Id == null || Id <= 0) return BadRequest();
//            if (!ModelState.IsValid)
//            {
//                return View(vm);
//            }
//            var data = await _db.Categories.FindAsync(Id);
//            if (data == null) return NotFound();

//            await _db.SaveChangesAsync();
//            TempData["Response"] = true;
//            return RedirectToAction(nameof(Index));
//        }

//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBackEnd.Contexts;
using PustokBackEnd.ViewModels.CategoryVM;
using System.Linq;
using System.Threading.Tasks;

namespace PustokBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PustokDBContext _db;

        public CategoryController(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories
                .Select(c => new CategoryListItemVM { Id = c.Id, Name = c.Name })
                .ToListAsync();

            return View(categories);
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
                ModelState.AddModelError("Name", "This Name already exists");
                return View(vm);
            }

            var category = new Models.Category { Name = vm.Name };
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var data = await _db.Categories.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(data);
            await _db.SaveChangesAsync();

            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }

            var data = await _db.Categories.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            var vm = new CategoryUpdateVM
            {
                Id = data.Id,
                Name = data.Name,
                ParentCategoryId = data.ParentCategoryId,
                // Products = data.Products, // Consider whether you really want to include products in the update view
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var data = await _db.Categories.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            data.Name = vm.Name;
            data.ParentCategoryId = vm.ParentCategoryId;

            await _db.SaveChangesAsync();

            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
