using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBackEnd.Contexts;

namespace PustokBackEnd.Controllers
{
    public class HomeController : Controller
    {
        PustokDBContext _database { get; }

        public HomeController(PustokDBContext database)
        {
            _database = database;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _database.Sliders.ToListAsync();
            return View(sliders);
        }
    }
}
