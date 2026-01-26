using BookNest.Constants;
using BookNest.Data;
using BookNest.Repositories;
using BookNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILibraryService _libraryService;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutsController(
            IBookService bookService,
            ILibraryService libraryService,
            UserManager<IdentityUser> userManager
        )
        {
            _bookService = bookService;
            _libraryService = libraryService;
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> AllCheckouts()
        {
            // TODO: return all checkouts
            var allCheckouts = await _libraryService.GetAllCheckouts();

            return View(allCheckouts);
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> OverdueCheckouts()
        {
            // TODO: return all checkouts
            var overdueCheckouts = await _libraryService.GetOverdueCheckouts();

            return View(overdueCheckouts);
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> PreviousCheckouts()
        {
            var memberCheckouts = await _libraryService.GetMemberCheckoutHistory(
                _userManager.GetUserId(User)
            );
            return View(memberCheckouts);
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> ActiveCheckouts()
        {
            var memberCheckouts = await _libraryService.GetMemberActiveCheckouts(
                _userManager.GetUserId(User)
            );
            return View(memberCheckouts);
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> Checkout(int bookId)
        {
            // TODO: Checkout book for memeber
            // TODO: check business roles

            var memeberCheckouts = await _libraryService.GetMemberActiveCheckouts(
                _userManager.GetUserId(User)
            );

            if (memeberCheckouts.Count() == 5)
            {
                // TODO message cant checkout book
                return RedirectToAction("Books", "Index");
            }

            if (!await _bookService.BookExisits(bookId))
            {
                return NotFound();
            }

            await _libraryService.CheckoutBook(bookId, _userManager.GetUserId(User));

            return RedirectToAction("Books", "Index");
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> Return(int checkoutId)
        {
            await _libraryService.ReturnBook(checkoutId);

            return RedirectToAction("Books", "Index");
        }
    }
}
