
using BookLib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookLib.ViewModel
{
    public class BookVM
    {

            public int Id { get; set; }
            [MaxLength(100)]
            public string Title { get; set; } = null!;

            public string Author { get; set; } = null!;
        
            public string Publisher { get; set; } = null!;

            public DateTime PublishDate { get; set; } = DateTime.Now;
          
            public string? ImageURL { get; set; }

            public List<string> Categories { get; set; } = null!;

    }
    }

