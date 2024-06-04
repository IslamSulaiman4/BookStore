using BookLib.Data;
using BookLib.Models;
using BookLib.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookLib.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            var categoriesVM = categories.Select(category => new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                UpdatedOn = category.UpdatedOn,
            }).ToList();

            return View(categoriesVM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryVM);
            }
            var category = new Category
            {
                Name = categoryVM.Name
            };

            try
            {
				context.Categories.Add(category);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
            catch
            {
                ModelState.AddModelError("Name", "Category name already exists");
                return View(categoryVM);
            }

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryVM = new CategoryVM
            {
                Id = id,
                Name = category.Name
            };
            return View("Create", categoryVM);

        }
        [HttpPost]
        public IActionResult Edit(CategoryVM categoryvm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryvm);
            }

            var category = context.Categories.Find(categoryvm.Id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
				category.Name = categoryvm.Name;
				category.UpdatedOn = DateTime.Now;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
            catch
            {
				ModelState.AddModelError("Name", "Category name already exists");
				return View("Create", categoryvm);
			}

		}
        public IActionResult Details(int id)
        {
            var category=context.Categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                UpdatedOn = category.UpdatedOn,
            };

            return View(categoryVM);
        }
        public IActionResult Delete(int id)
        {
			var category = context.Categories.Find(id);
			if (category is null)
			{
				return NotFound();
			}
            context.Categories.Remove(category);
            context.SaveChanges();
            return Ok();
		}
    }
}
