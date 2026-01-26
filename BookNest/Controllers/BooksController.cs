using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BookNest.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly UserManager<IdentityUser> _userManager;

        public BooksController(
            IBookService bookService,
            IAuthorService authorService,
            UserManager<IdentityUser> userManager
        )
        {
            _bookService = bookService;
            _authorService = authorService;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Book> books = new List<Book>();

            // Logged in + Member role
            if (User.IsInRole(Roles.Member))
            {
                books = await _bookService.GetBooksForMember();
            }

            // Logged in + Librarian role
            if (User.IsInRole(Roles.Librarian))
            {
                books = await _bookService.GetBooksForLibrarian();
            }

            return View(books);
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Create()
        {
            var bookVm = new BookCreateViewModel
            {
                Authors = await _authorService.BuildAuthorDropDownList(),
            };
            return View(bookVm);
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel bookVm)
        {


            if (!ModelState.IsValid)
                return View("Create", bookVm);

            // await _bookService.AddNewBook(book);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Edit(int id)
        {
            var bookInDb = await _bookService.GetBookById(id);

            if (bookInDb == null)
                return NotFound();

            return View(bookInDb);
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            ModelState.Remove("Author"); // TODO use ViewModel?

            if (!ModelState.IsValid)
                return View(book);

            await _bookService.UpdateBook(book);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookInDb = await _bookService.GetBookById(id);

            if (bookInDb == null)
                return NotFound();

            // TODO: if books is checked out not able to delete
            if (!bookInDb.IsAvailable)
            {
                // TODO: confirm boook is not cheked out, if so error and tell user
                // message cant delete is checkedout
            }
            else
            {
                await _bookService.DeleteBook(bookInDb.Id);
            }

            return RedirectToAction("Index");
        }
    }
}
