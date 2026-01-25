using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        // private readonly

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> Index()
        {
            // TODO: retreive all authors
            var authors = await _authorService.GetAllAuthors();

            return View(authors);
        }

        [Authorize(Roles = Roles.Librarian)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
                return View("Create", author);

            await _authorService.AddNewAuthor(author);

            return RedirectToAction(nameof(Index));
        }

        // [Authorize(Roles = Roles.Librarian)]
        // public IActionResult Edit()
        // {
        //     return View();
        // }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Edit(int id)
        {
            // TODO: retrieve data for author by Id
            var authorInDb = await _authorService.GetAuthorById(id);

            if (authorInDb == null)
                return NotFound();

            return View(authorInDb);
        }

        [Authorize(Roles = Roles.Librarian)]
        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);

            await _authorService.UpdateAuthor(author);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> Delete(int id)
        {
            var authorInDb = await _authorService.GetAuthorById(id);

            if (authorInDb == null)
                return NotFound();

            // TODO: if author has books not able to delete
            var hasbooks = false;
            if (hasbooks)
            {
                // message cant delete has books
            }
            else
            {
                await _authorService.DeleteAuthor(authorInDb.Id);
            }

            return RedirectToAction("Index");
        }
    }
}
