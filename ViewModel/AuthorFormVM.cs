using System.ComponentModel.DataAnnotations;

namespace BookLib.ViewModel
{
	public class AuthorFormVM
	{
		public int Id { get; set; }

		[MaxLength(50, ErrorMessage="Name field can not exceeds 50 characters")]
		public string Name { get; set; }
	}
}
