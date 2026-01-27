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

        public BooksController(
            IBookService bookService,
            IAuthorService authorService
        )
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<BookListItemViewModel> booksVmList = new List<BookListItemViewModel>();

            // Logged in + Member role
            if (User.IsInRole(Roles.Member))
            {
                booksVmList = await _bookService.GetBooksForMember();
            }

            // Logged in + Librarian role
            if (User.IsInRole(Roles.Librarian))
            {
                booksVmList = await _bookService.GetBooksForLibrarian();
            }

            return View(booksVmList);
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
                return View(bookVm);

            await _bookService.AddNewBook(bookVm);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Edit(int id)
        {
            var bookInDb = await _bookService.GetBookById(id);

            if (bookInDb == null)
                return NotFound();

            var bookVm = await _bookService.BuildEditViewModel(bookInDb);

            return View(bookVm);
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel bookVm)
        {
            if (!ModelState.IsValid)
                return View(bookVm);

            await _bookService.UpdateBook(bookVm);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookInDb = await _bookService.GetBookById(id);

            if (bookInDb == null)
                return NotFound();

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
