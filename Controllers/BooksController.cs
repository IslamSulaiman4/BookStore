using BookLib.Data;
using BookLib.Models;
using BookLib.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BookLib.Controllers
{
	public class BooksController : Controller
	{
		private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
		{
			this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

		public IActionResult Index()
		{
			var bookVM = context.Books
				.Include(book=>book.Author)
				.Include(book=>book.Categories)
				.ThenInclude(book=>book.Category).
				ToList().Select(book => new BookVM
			{
				Id = book.Id,
				Title = book.Title,
				Author= book.Author.Name,
				PublishDate = book.PublishDate,
				Publisher = book.Publisher,
				ImageURL = book.ImageURL,
				Categories = book.Categories.Select(book => book.Category.Name).ToList()

			}).ToList();


			return View(bookVM);
		}
		[HttpGet]
		public IActionResult Create()
		{
			var authors=context.Authors.OrderBy(author=>author.Name).ToList();
            var categories = context.Categories.OrderBy(author => author.Name).ToList();

			var authorList = authors.Select(author => new SelectListItem
			{
				Value = author.Id.ToString(),
				Text = author.Name,
			}).ToList();

			var categoryList = categories.Select(category => new SelectListItem
			{
				Value = category.Id.ToString(),
				Text = category.Name,
			}).ToList();


            var viewModel = new BookFormVM
			{
				Authors= authorList,
				Categories= categoryList,
			};
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult Create(BookFormVM viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}
			string  ImageName = null;
			if(viewModel.ImageURL != null)
			{
			 ImageName = Path.GetFileName(viewModel.ImageURL.FileName);
				var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/Book", ImageName);
				var stream=System.IO.File.Create(path);
				viewModel.ImageURL.CopyTo(stream);
			}
			var book = new Book
			{
				Title = viewModel.Title,
				AuthorId = viewModel.AuthorId,
				PublishDate = viewModel.PublishDate,
				Publisher = viewModel.Publisher,
				Description = viewModel.Description,
				ImageURL=ImageName,
				Categories = viewModel.selectedCategories.Select(id => new BookCategory
				{
					CategoryId = id,
				}).ToList(),
			};

			context.Books.Add(book);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

	public IActionResult Delete(int id)
		{
			var book=context.Books.Find(id);
			if (book is null) {
				return NotFound();

			}
			var path = Path.Combine(webHostEnvironment.WebRootPath,"img/Book",book.ImageURL);
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}


			context.Books.Remove(book);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}

}
