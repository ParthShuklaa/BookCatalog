using System;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; } = string.Empty;
        
        public string Genre { get; set; } = string.Empty;
        
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}