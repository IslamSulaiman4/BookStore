using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookLib.Models
{
    [Index(nameof(Name), IsUnique = true)]

    public class Author
    {

        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Name field can not exceeds 50 characters")]
        public string Name { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public DateTime UpdatedOn { get; set; } = DateTime.Now;
	}
}
