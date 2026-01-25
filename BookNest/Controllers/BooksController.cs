using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BookNest.Controllers
{
    public class BooksController : Controller
    {
        
        public async Task<ActionResult> Index()
        {
            // TODO: retreive all available books for memebers
            // TODO: retreive all books for librarians

            return View();
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> CreateBook()
        {
            return View(new Book());
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> EditBook(int? id)
        {
            if (id != null)
            {
                // TODO: retrieve data for book by Id
                var bookInDb = new Book
                {
                    Id = 1,
                    Title = "Test book",
                    AuthorId = 1,
                };

                return View(bookInDb);
            }
            return View(new Book());
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO: confirm boook is not cheked out, if so error and tell user

            // TODO: if book avaiable, able to delete

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> HandleCreateBookFormSubmit(Book book)
        {
            if (!ModelState.IsValid)
                return View("CreateBook", book);

            // TODO: validate data then try and save data to DB

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> HandleEditBookFormSubmit(Book book)
        {
            if (!ModelState.IsValid)
                return View("CreateBook", book);

            // TODO: validate data then try and update data to DB

            return RedirectToAction("Index");
        }
    }
}
