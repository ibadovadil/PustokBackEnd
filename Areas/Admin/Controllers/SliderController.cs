using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBackEnd.Contexts;
using PustokBackEnd.Models;
using PustokBackEnd.ViewModels.SliderVM;
using System.Collections.Generic;

namespace PustokBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        PustokDBContext _database { get; }

        public SliderController(PustokDBContext database)
        {
            _database = database;
        }

        public async Task<IActionResult> Index()
        {
            //var slider = await dbContext.Sliders.ToListAsync();
            //List<SliderListItemVM> items = new List<SliderListItemVM>();
            //foreach (var sliderItem in slider)
            //{
            //    items.Add(new SliderListItemVM
            //    {
            //        Id = sliderItem.Id,
            //        ImageUrl = sliderItem.ImageUrl,
            //        Text = sliderItem.Text,
            //        Title = sliderItem.Title,
            //        IsLeft = sliderItem.IsLeft,
            //        IsRightText = sliderItem.IsRightText,
            //    });
            //}

            var items = await _database.Sliders.Select(s => new SliderListItemVM
            {
                Title = s.Title,
                Text = s.Text,
                IsLeft = s.IsLeft,
                IsRightText = s.IsRightText,
                Id = s.Id,
                ImageUrl = s.ImageUrl,
            }).ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(CreateSliderItemVM vm)
        {
            if (vm.IsLeft < 0 || vm.IsLeft > 1)
            {
                ModelState.AddModelError("IsLeft", "Wrong Input");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                Text = vm.Text,
                Title = vm.Title,
                ImageUrl = vm.ImageUrl,
                IsLeft = vm.IsLeft switch
                {
                    0 => true,
                    1 => false,
                },
                IsRightText = vm.IsRightText switch
                {
                    0 => true,
                    1 => false,
                },
            };
            await _database.Sliders.AddAsync(slider);
            await _database.SaveChangesAsync();
            return View();
        }



        //public async Task<IActionResult> Create(Slider slider)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(slider);
        //    }
        //    using PustokDBContext pustokDBContext = new PustokDBContext();
        //    await pustokDBContext.Sliders.AddAsync(slider);
        //    await pustokDBContext.SaveChangesAsync();
        //    return View();
        //}
        public async Task<IActionResult> Delete(int? Id)
        {
            TempData["Respnose"] = false;
            if (Id == null) return BadRequest();
            var data = await _database.Sliders.FindAsync(Id);
            if (data == null) return NotFound();
            _database.Sliders.Remove(data);
            await _database.SaveChangesAsync();
            TempData["Respnose"] = true;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null || Id <= 0) return BadRequest();
            var data = await _database.Sliders.FindAsync(Id);
            if (data == null) return NotFound();
            return View(new SliderUpdateVM
            {
                ImageUrl = data.ImageUrl,
                Text = data.Text,
                Title = data.Title,
                IsLeft = data.IsLeft switch
                {
                    true => 1,
                    false => 0,
                },
                IsRightText = data.IsRightText switch
                {
                    true => 1,
                    false => 0,
                },
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? Id, SliderUpdateVM vm)
        {
            if (Id == null || Id <= 0) return BadRequest();
            if (vm.IsLeft < 0 || vm.IsLeft > 1)
            {
                ModelState.AddModelError("IsLeft", "Wrong Input");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _database.Sliders.FindAsync(Id);
            if (data == null) return NotFound();
            data.Text = vm.Text;
            data.Title = vm.Title;
            data.ImageUrl = vm.ImageUrl;
            data.IsLeft = vm.IsLeft switch
            {
                0 => true,
                1 => false,
            };
            data.IsRightText = vm.IsRightText switch
            {
                0 => true,
                1 => false,
            };
            await _database.SaveChangesAsync();
            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
