using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> Index()
        {
            // TODO: retreive all authors
            return View();
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> CreateAuthor()
        {
            return View(new Author());
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> EditAuthor(int? id)
        {
            if (id != null)
            {
                // TODO: retrieve data for author by Id
                var authorInDb = new Author { Id = 1, FullName = "TestA" };

                return View(authorInDb);
            }
            return View(new Book());
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> DeleteAuthor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO: if author has books not able to delete

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> HandleCreateAuthorFormSubmit(Author author)
        {
            if (!ModelState.IsValid)
                return View("Create", author);

            // TODO: validate data then try and save data to DB

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<IActionResult> HandleEditAuthorFormSubmit(Author author)
        {
            if (!ModelState.IsValid)
                return View("Edit", author);

            // TODO: validate data then try and update data to DB

            return RedirectToAction("Index");
        }
    }
}
