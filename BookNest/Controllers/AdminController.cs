using BookNest.Constants;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILibrarianService _librarianService;

        public AdminController(ILibrarianService librarianService)
        {
            _librarianService = librarianService;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var librarians = await _librarianService.GetAllAsync();
                return View(librarians);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load librarians.";
                return View(new List<LibrarianListViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLibrarianViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Please fill in all the fields";
                return View(model);
            }

            try
            {
                var success = await _librarianService.CreateAsync(model);

                TempData["Success"] = "Librarian created successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save librarian. Please try again.");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _librarianService.DeleteAsync(id);
                TempData["Success"] = "Librarian deleted successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to delete librarian.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
