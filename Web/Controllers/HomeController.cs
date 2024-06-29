using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataBase;
using DataBase.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
	public class HomeController(DogsDBContext context,IMapper maper) : Controller
	{
		

		public async Task<IActionResult> Index()
		{
			var dogs = await context.Dogs.ProjectTo<DogViewModel>(maper.ConfigurationProvider).ToArrayAsync();
			return View(dogs);
		}

        public IActionResult Delete(DogViewModel dog)
        {
			return View(dog);
        }

        public IActionResult Edit(DogViewModel dog)
        {
            return View(dog);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(DogViewModel dog)
        {
			context.Update(maper.Map<Dog>(dog));
			await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int Id)
        {
			var dog = await context.Dogs.FirstOrDefaultAsync(x => x.Id == Id);
			if (dog != null) 
			{
                context.Remove(dog);
				await context.SaveChangesAsync();
            }
		    return  RedirectToAction("Index");
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
