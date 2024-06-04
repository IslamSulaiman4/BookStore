using BookLib.Data;
using BookLib.Models;
using BookLib.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookLib.Controllers
{
	public class AuthorsController : Controller
	{
		private readonly ApplicationDbContext context;

		public AuthorsController(ApplicationDbContext context)
		{
			this.context = context;
		}
		public IActionResult Index()
		{
			var authors = context.Authors.ToList();
			var authorsVM = authors.Select(author=>new AuthorVM
			{
				Id = author.Id,
				Name = author.Name,
				CreatedOn = author.CreatedOn,
				UpdatedOn = author.UpdatedOn,
			}).ToList();


			return View(authorsVM);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View("Form");
		}
		[HttpPost]
		public IActionResult Create(AuthorFormVM authorVM)
		{
			if (!ModelState.IsValid)
			{
				return View("Form", authorVM);
			}

			var author = new Author
			{
				Name = authorVM.Name
			};
			try
			{
				context.Authors.Add(author);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{
				ModelState.AddModelError("Name", "Author name already exists");
				return View("Form", authorVM);
			}
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var author = context.Authors.Find(id);
			if (author == null)
			{
				return NotFound();
			}
			var authorVM = new AuthorFormVM()
			{
				Id = id,
				Name = author.Name,
			};
			return View("Form", authorVM);

		}
		[HttpPost]
		public IActionResult Edit(AuthorFormVM authorVM)
		{
			if (!ModelState.IsValid)
			{
				return View("Form", authorVM);
			}
			var author = context.Authors.Find(authorVM.Id);
			if (author == null)
			{
				return NotFound();
			}
			try
			{
				author.Name = authorVM.Name;
				author.UpdatedOn = DateTime.Now;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{
				ModelState.AddModelError("Name", "Author name already exists");
				return View("Form", authorVM);
			}


		}
		public IActionResult Details(int id)
		{
			var author = context.Authors.Find(id);
			if (author == null)
			{
				return NotFound();
			}
			var authorVM = new AuthorVM
			{
				Id = author.Id,
				Name = author.Name,
				CreatedOn = author.CreatedOn,
				UpdatedOn = author.UpdatedOn,
			};

			return View(authorVM);

		}
		public IActionResult Delete(int id)
		{
			var author = context.Authors.Find(id);
			if (author == null)
			{
				return NotFound();
			}
			context.Authors.Remove(author);
			context.SaveChanges();
			return Ok();
		}
	}
}
