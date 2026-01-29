using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BookNest.Controllers
{
    [Authorize(Roles = Roles.Librarian)]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public AuthorsController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var authors = await _authorService.GetAllAuthors();
                return View(authors);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load authors.";
                return View(new List<Author>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Please fill in all the fields";
                return View("Create", author);
            }

            try
            {
                await _authorService.AddNewAuthor(author);
                TempData["Success"] = "Author created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save author. Please try again.");
                return View(author);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var authorInDb = await _authorService.GetAuthorById(id);
                return View(authorInDb);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load author for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Please fill in all the fields";
                return View(author);
            }

            try
            {
                await _authorService.UpdateAuthor(author);
                TempData["Success"] = "Author updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to update author. Please try again.");
                return View(author);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var authorInDb = await _authorService.GetAuthorById(id);

                var authorBooks = await _bookService.GetBooksByAuthor(id);
                if (authorBooks.Any())
                {
                    TempData["Info"] = "Cannot delete author with existing books.";
                }
                else
                {
                    await _authorService.DeleteAuthor(authorInDb.Id);
                    TempData["Success"] = "Author deleted successfully!";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to delete author.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
