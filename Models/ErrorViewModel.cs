using System.Collections.Generic;

namespace BookCatalog.Models
{
    public class BookIndexViewModel
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public Book NewBook { get; set; } = new Book();
    }
}