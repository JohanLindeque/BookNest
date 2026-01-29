using BookNest.Constants;
using BookNest.Data;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibrarianController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var checkouts = await _libraryService.GetOverdueCheckouts();
                return View(checkouts);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load overdue checkouts.";
                return View(new List<CheckoutListViewModel>());
            }
        }
    }
}
