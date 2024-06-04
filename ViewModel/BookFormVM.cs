using BookLib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookLib.ViewModel
{
	public class BookFormVM
	{
		public int Id {  get; set; }
		[MaxLength(100)]
		public string Title { get; set; } = null!;

		[Display(Name ="Author")]
		public int AuthorId { get; set; }
		public List <SelectListItem> ?Authors { get; set; }
        public string Publisher { get; set; } = null!;
		[Display(Name ="Publish Date")]
        public DateTime PublishDate { get; set; }= DateTime.Now;
		[Display(Name ="Image for the book")]
        public IFormFile? ImageURL { get; set; }
        public string Description { get; set; } = null!;
		[Display(Name ="Categories")]
		public List<int> selectedCategories { get; set; }=new List<int>();
        public List<SelectListItem>? Categories { get; set; }

    }
}
