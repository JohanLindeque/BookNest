using System.ComponentModel.Design;
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

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<ActionResult> Index()
        {
            try
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
            catch (Exception)
            {
                TempData["Error"] = "Failed to load dashboard data.";
                return View(new List<BookListItemViewModel>());
            }
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Create()
        {
            try
            {
                var bookVm = new BookCreateViewModel
                {
                    Authors = await _authorService.BuildAuthorDropDownList(),
                };
                return View(bookVm);
            }
            catch (Exception)
            {
                TempData["Error"] = "Unable to crate book. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel bookVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Please fill in all the fields";
                return View(bookVm);
            }

            try
            {
                await _bookService.AddNewBook(bookVm);
                TempData["Success"] = "Book created successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save book. Please try again.");
                return View(bookVm);
            }
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var bookInDb = await _bookService.GetBookById(id);

                if (bookInDb == null)
                    return NotFound();

                var bookVm = await _bookService.BuildEditViewModel(bookInDb);

                return View(bookVm);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load book for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel bookVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Please fill in all the fields";
                return View(bookVm);
            }

            try
            {
                await _bookService.UpdateBook(bookVm);
                TempData["Success"] = "Book updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to update book. Please try again.");
                return View(bookVm);
            }
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bookInDb = await _bookService.GetBookById(id);

                if (bookInDb == null)
                    return NotFound();

                if (!bookInDb.IsAvailable)
                {
                    TempData["Info"] = "Cannot delete book that has not been returned.";
                }
                else
                {
                    await _bookService.DeleteBook(bookInDb.Id);
                    TempData["Success"] = "Book deleted successfully!";
                }

                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to delete book.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
