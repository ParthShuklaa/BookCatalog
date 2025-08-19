using BookCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookCatalog.Controllers
{
    public class BooksController : Controller
    {
        // In-memory store for demo purposes
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", PublishedDate = new DateTime(1925, 4, 10) },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", PublishedDate = new DateTime(1960, 7, 11) },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Genre = "Dystopian", PublishedDate = new DateTime(1949, 6, 8) }
        };
        private static int _nextId = 4;

        public IActionResult Index()
        {
            var viewModel = new BookIndexViewModel
            {
                Books = _books
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook(Book newBook)
        {
            if (ModelState.IsValid)
            {
                newBook.Id = _nextId++;
                _books.Add(newBook);
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_BookCard", newBook);
                }
                
                return RedirectToAction(nameof(Index));
            }
            
            // If validation fails, return to index with errors
            var viewModel = new BookIndexViewModel
            {
                Books = _books,
                NewBook = newBook
            };
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBook = _books.FirstOrDefault(b => b.Id == id);
                if (existingBook == null)
                {
                    return NotFound();
                }
                
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                existingBook.PublishedDate = book.PublishedDate;
                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}